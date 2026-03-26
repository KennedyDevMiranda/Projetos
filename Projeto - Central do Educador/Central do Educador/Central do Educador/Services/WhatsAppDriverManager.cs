using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using QRCoder;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Central_do_Educador.Services
{
    /// <summary>
    /// Gerencia uma única instância do Selenium/EdgeDriver para o WhatsApp Web.
    /// O driver é compartilhado por todos os formulários e só é encerrado ao fechar a aplicação.
    /// </summary>
    public static class WhatsAppDriverManager
    {
        private static IWebDriver? _driver;
        private static volatile bool _conectado;
        private static volatile bool _inicializando;
        private static readonly object _lock = new();
        private static readonly QRCodeGenerator _qrGenerator = new();
        private static readonly Random _rnd = new();

        // ── Estado público ──
        public static bool Conectado => _conectado;
        public static bool Inicializando => _inicializando;
        public static IWebDriver? Driver => _driver;

        /// <summary>Número do celular conectado ao WhatsApp Web (ex: 5511999999999).</summary>
        public static string? NumeroConectado { get; private set; }

        /// <summary>
        /// Tenta obter o número do celular conectado ao WhatsApp Web via localStorage.
        /// </summary>
        public static string? ObterNumeroConectado()
        {
            try
            {
                if (_driver == null || !_conectado) return null;

                var js = (IJavaScriptExecutor)_driver;

                // WhatsApp Web armazena o WID no localStorage
                var wid = js.ExecuteScript(
                    "return localStorage.getItem('last-wid-md') || localStorage.getItem('last-wid');")
                    as string;

                if (!string.IsNullOrWhiteSpace(wid))
                {
                    // Formato: "5511999999999@c.us" → extrair apenas o número
                    var numero = wid.Split('@')[0].Split(':')[0];
                    NumeroConectado = numero;
                    return numero;
                }
            }
            catch { }

            return null;
        }

        // ── Eventos para a UI ──
        /// <summary>Disparado quando a conexão com o WhatsApp é estabelecida.</summary>
        public static event Action? OnConectado;

        /// <summary>Disparado quando é detectado que precisa de QR Code (não está logado).</summary>
        public static event Action? OnDesconectado;

        /// <summary>Disparado quando um novo QR Code está disponível para exibição.</summary>
        public static event Action<Image>? OnQRCodeDisponivel;

        /// <summary>
        /// Inicia ou reutiliza a conexão com o WhatsApp Web.
        /// Se já estiver conectado, não faz nada. Se já estiver inicializando, aguarda.
        /// </summary>
        /// <param name="exibirQR">Se true, exibe QR Code caso necessário. Se false, fecha o driver ao encontrar QR.</param>
        public static async Task IniciarAsync(bool exibirQR = false, int tentativa = 1)
        {
            // Já conectado → nada a fazer
            if (_conectado && _driver != null)
            {
                OnConectado?.Invoke();
                return;
            }

            // Já está inicializando em outra thread → aguarda
            lock (_lock)
            {
                if (_inicializando) return;
                _inicializando = true;
            }

            try
            {
                try
                {
                    if (tentativa > 1)
                    {
                        foreach (var p in Process.GetProcessesByName("msedge")) p.Kill();
                    }
                    foreach (var p in Process.GetProcessesByName("msedgedriver")) p.Kill();
                }
                catch { }

                new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);

                var options = new EdgeOptions();

                string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string pastaSessao = Path.Combine(caminhoAppData, "ChatBotWhatsApp", "DadosDoPerfil");

                if (!Directory.Exists(pastaSessao))
                    Directory.CreateDirectory(pastaSessao);

                options.AddArgument($"--user-data-dir={pastaSessao}");
                options.AddArgument("--profile-directory=Default");
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36");

                var service = EdgeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                _driver = new EdgeDriver(service, options);
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);

                _driver.Navigate().GoToUrl("https://web.whatsapp.com");

                // Loop de detecção
                _ = Task.Run(async () =>
                {
                    var currentDriver = _driver;
                    while (currentDriver != null && !_conectado)
                    {
                        try
                        {
                            var logado = currentDriver.FindElements(By.XPath(
                                "//div[@id='pane-side'] | //div[@contenteditable='true'][@data-tab='3']")).Count > 0;

                            if (logado)
                            {
                                _conectado = true;
                                _inicializando = false;
                                OnConectado?.Invoke();
                                break;
                            }

                            var qrElements = currentDriver.FindElements(By.XPath("//div[@data-ref]"));
                            if (qrElements.Count > 0)
                            {
                                if (exibirQR)
                                {
                                    string? tokenQR = qrElements[0].GetAttribute("data-ref");
                                    if (!string.IsNullOrWhiteSpace(tokenQR))
                                    {
                                        Image imgQR = GerarImagemQR(tokenQR!);
                                        OnQRCodeDisponivel?.Invoke(imgQR);
                                    }
                                }
                                else
                                {
                                    // QR necessário mas não solicitado → fecha e aguarda clique do usuário
                                    try { currentDriver.Quit(); } catch { }
                                    _driver = null;
                                    _inicializando = false;
                                    OnDesconectado?.Invoke();
                                    break;
                                }
                            }
                        }
                        catch { }
                        await Task.Delay(2000);
                        currentDriver = _driver;
                    }
                });
            }
            catch (Exception)
            {
                _inicializando = false;

                if (tentativa < 2)
                {
                    await Task.Delay(2000);
                    await IniciarAsync(exibirQR, 2);
                }
                else
                {
                    OnDesconectado?.Invoke();
                }
            }
        }

        /// <summary>
        /// Envia uma mensagem via WhatsApp Web (Selenium).
        /// </summary>
        public static async Task EnviarMensagemAsync(string numero, string texto)
        {
            var driver = _driver;
            if (driver == null || !_conectado)
                throw new Exception("WhatsApp não conectado.");

            string num = new string(numero.Where(char.IsDigit).ToArray());
            if (num.Length is 10 or 11) num = "55" + num;

            driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={num}");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));

            IWebElement campo = wait.Until(d =>
            {
                try
                {
                    var erroJanela = d.FindElements(By.XPath(
                        "//div[contains(text(), 'inválido') or contains(text(), 'invalid') or contains(text(), 'URL')]"));
                    if (erroJanela.Count > 0)
                        throw new Exception("O número digitado é inválido.");

                    var el = d.FindElements(By.XPath("//footer//div[@contenteditable='true']")).FirstOrDefault();
                    if (el != null && el.Displayed && el.Enabled) return el;
                }
                catch (Exception ex) when (!ex.Message.Contains("O número digitado é inválido"))
                {
                }
                return null;
            });

            await Task.Delay(1000);
            campo.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string textoFormatado = texto.Replace("\r\n", "\n").Replace("\r", "\n");

            js.ExecuteScript(@"
                var el = arguments[0];
                var txt = arguments[1];
                el.focus();
                const dataTransfer = new DataTransfer();
                dataTransfer.setData('text/plain', txt);
                const event = new ClipboardEvent('paste', { clipboardData: dataTransfer, bubbles: true });
                el.dispatchEvent(event);
                el.dispatchEvent(new Event('input', { bubbles: true }));
            ", campo, textoFormatado);

            await Task.Delay(1000);

            IWebElement btnEnviar = wait.Until(d =>
            {
                var btn = d.FindElement(By.XPath(
                    "//button[@aria-label='Enviar'] | //span[@data-icon='send']/parent::button"));
                return (btn.Displayed && btn.Enabled) ? btn : null;
            });

            btnEnviar.Click();
            await Task.Delay(2000);
        }

        /// <summary>
        /// Desconecta e limpa os dados de sessão (força novo QR Code).
        /// </summary>
        public static void Desconectar(bool limparSessao = true)
        {
            _conectado = false;
            _inicializando = false;

            if (_driver != null)
            {
                try { _driver.Quit(); } catch { }
                _driver = null;
            }

            if (limparSessao)
            {
                string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string pastaSessao = Path.Combine(caminhoAppData, "ChatBotWhatsApp", "DadosDoPerfil");

                if (Directory.Exists(pastaSessao))
                {
                    Thread.Sleep(1000);
                    try { Directory.Delete(pastaSessao, true); } catch { }
                }
            }

            OnDesconectado?.Invoke();
        }

        /// <summary>
        /// Encerra o driver. Deve ser chamado apenas ao fechar a aplicação.
        /// </summary>
        public static void Encerrar()
        {
            _conectado = false;
            _inicializando = false;

            if (_driver != null)
            {
                try { _driver.Quit(); } catch { }
                _driver = null;
            }
        }

        private static Image GerarImagemQR(string texto)
        {
            using var qrData = _qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            return qrCode.GetGraphic(20);
        }
    }
}
