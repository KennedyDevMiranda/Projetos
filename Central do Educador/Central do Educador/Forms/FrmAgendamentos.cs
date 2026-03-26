using Central_do_Educador.Data;
using Central_do_Educador.Models;
using Central_do_Educador.Services;
using Central_do_Educador.Forms;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Central_do_Educador
{
    public partial class FrmAgendamentos : Form
    {
        // Ajuste para o seu caminho/strategia de conexão
        //private readonly string _cs = "Data Source=agMsg.db";

        private AgendamentoService _service;

        private System.Windows.Forms.Timer _timer;
        private int? _idSelecionado = null;

        // evita rodar tick sobre tick (reentrância)
        private int _processing = 0;

        public FrmAgendamentos()
        {
            InitializeComponent();
        }

        private void FrmAgendamentos_Load(object sender, EventArgs e)
        {
            Db.InitializeAsync().Wait();
            _service = new AgendamentoService(Db.ConnectionString);

            // data/hora pickers
            dtpData.Format = DateTimePickerFormat.Short;

            dtpHora.Format = DateTimePickerFormat.Time;
            dtpHora.ShowUpDown = true;

            chkAtivo.Checked = true;

            ConfigurarGrid();
            CarregarGrid();

            // Timer (motor)
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 30000; // 30s
            _timer.Tick += async (s, ev) => await ProcessarEnviosAsync();
            _timer.Start();

            AtualizarStatusMotor("Motor ativo (a cada 30s)");
        }

        private void ConfigurarGrid()
        {
            dgvAgendamentos.AutoGenerateColumns = false;
            dgvAgendamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAgendamentos.MultiSelect = false;
            dgvAgendamentos.AllowUserToAddRows = false;
            dgvAgendamentos.ReadOnly = true;

            dgvAgendamentos.Columns.Clear();

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Width = 60
            });

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Destinatario",
                HeaderText = "Destinatário",
                Width = 160
            });

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Telefone",
                HeaderText = "Telefone",
                Width = 130
            });

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DataHoraAgendada",
                HeaderText = "Agendado para",
                Width = 160,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 90
            });

            dgvAgendamentos.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Ativo",
                HeaderText = "Ativo",
                Width = 60
            });

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UltimaTentativa",
                HeaderText = "Última tentativa",
                Width = 160,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvAgendamentos.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Erro",
                HeaderText = "Erro",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvAgendamentos.CellClick += DgvAgendamentos_CellClick;
        }

        private void CarregarGrid()
        {
            var lista = _service.ListarTodos(incluirCanceladas: true);
            dgvAgendamentos.DataSource = lista;
            lblTotal.Text = $"Total: {lista.Count}";
        }

        private void LimparFormulario()
        {
            _idSelecionado = null;
            txtDestinatario.Text = "";
            txtTelefone.Text = "";
            txtMensagem.Text = "";
            chkAtivo.Checked = true;

            dtpData.Value = DateTime.Now.Date;
            dtpHora.Value = DateTime.Now;

            btnSalvar.Text = "Salvar";
            btnCancelar.Enabled = false;
            btnReagendar.Enabled = false;
            btnCancelarAgendamento.Enabled = false;
        }

        private DateTime ObterDataHoraForm()
        {
            var data = dtpData.Value.Date;
            var hora = dtpHora.Value;
            return data.AddHours(hora.Hour).AddMinutes(hora.Minute);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var telefone = (txtTelefone.Text ?? "").Trim();
            var mensagem = (txtMensagem.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(telefone))
            {
                MessageBox.Show("Informe o telefone.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(mensagem))
            {
                MessageBox.Show("Informe a mensagem.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMensagem.Focus();
                return;
            }

            var dataHora = ObterDataHoraForm();

            // Evita agendar no passado (mas deixa você ajustar se quiser)
            if (dataHora < DateTime.Now.AddMinutes(-1))
            {
                var r = MessageBox.Show(
                    "Você está agendando no passado. Quer salvar mesmo assim?\n\nIsso pode fazer o motor disparar na próxima rodada.",
                    "Atenção",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (r != DialogResult.Yes) return;
            }

            var obj = new MensagemAgendada
            {
                Id = _idSelecionado ?? 0,
                Destinatario = (txtDestinatario.Text ?? "").Trim(),
                Telefone = telefone,
                Mensagem = mensagem,
                DataHoraAgendada = dataHora,
                Ativo = chkAtivo.Checked,
                Status = "Pendente"
            };

            try
            {
                if (_idSelecionado == null)
                {
                    var id = _service.Inserir(obj);
                    _idSelecionado = id;
                }
                else
                {
                    _service.Atualizar(obj);
                }

                CarregarGrid();
                btnCancelar.Enabled = true;
                btnReagendar.Enabled = true;
                btnCancelarAgendamento.Enabled = true;

                MessageBox.Show("Agendamento salvo!", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparFormulario();
        }

        private void btnRecarregar_Click(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void DgvAgendamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvAgendamentos.Rows[e.RowIndex];
            if (row?.DataBoundItem is not MensagemAgendada item) return;

            _idSelecionado = item.Id;

            txtDestinatario.Text = item.Destinatario;
            txtTelefone.Text = item.Telefone;
            txtMensagem.Text = item.Mensagem;
            chkAtivo.Checked = item.Ativo;

            dtpData.Value = item.DataHoraAgendada.Date;
            dtpHora.Value = item.DataHoraAgendada;

            btnSalvar.Text = "Atualizar";
            btnCancelar.Enabled = true;
            btnReagendar.Enabled = true;
            btnCancelarAgendamento.Enabled = true;
        }

        private void btnCancelarAgendamento_Click(object sender, EventArgs e)
        {
            if (_idSelecionado == null)
            {
                MessageBox.Show("Selecione um agendamento no grid.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var r = MessageBox.Show(
                "Cancelar este agendamento?\n\nEle ficará como 'Cancelada' e não será mais enviado.",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                _service.Cancelar(_idSelecionado.Value);
                CarregarGrid();
                LimparFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cancelar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReagendar_Click(object sender, EventArgs e)
        {
            if (_idSelecionado == null)
            {
                MessageBox.Show("Selecione um agendamento no grid.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var novaDataHora = ObterDataHoraForm();

            try
            {
                _service.Reagendar(_idSelecionado.Value, novaDataHora);
                CarregarGrid();
                MessageBox.Show("Reagendado! Agora voltou para 'Pendente'.", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reagendar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnProcessarAgora_Click(object sender, EventArgs e)
        {
            await ProcessarEnviosAsync();
        }

        private async Task ProcessarEnviosAsync()
        {
            // trava reentrância
            if (Interlocked.Exchange(ref _processing, 1) == 1) return;

            try
            {
                AtualizarStatusMotor("Processando pendentes...");

                var pendentes = _service.BuscarPendentesParaEnviar(limite: 50);
                if (pendentes.Count == 0)
                {
                    AtualizarStatusMotor("Sem pendentes no momento.");
                    return;
                }

                foreach (var msg in pendentes)
                {
                    try
                    {
                        await WhatsAppService.EnviarAsync(msg.Telefone, msg.Mensagem);
                        _service.MarcarTentativa(msg.Id, sucesso: true);
                    }
                    catch (Exception ex)
                    {
                        _service.MarcarTentativa(msg.Id, sucesso: false, erro: ex.Message);
                    }
                }

                CarregarGrid();
                AtualizarStatusMotor($"Processado: {pendentes.Count} pendente(s). Última rodada: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            }
            finally
            {
                Interlocked.Exchange(ref _processing, 0);
            }
        }

        private void AtualizarStatusMotor(string texto)
        {
            lblMotor.Text = texto;
        }

        private void FrmAgendamentos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _timer?.Stop();
                _timer?.Dispose();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bot = new FrmChatBot();
            bot.ShowDialog();
        }
    }
}