using System;
using System.Security.Cryptography;

namespace Central_do_Educador.Security
{
    public static class SenhaHasher
    {
        // Formato: {iter}.{saltBase64}.{hashBase64}
        public static string GerarHash(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha inválida.");

            const int iter = 100_000;
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iter, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return $"{iter}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verificar(string senha, string senhaHash)
        {
            try
            {
                var parts = senhaHash.Split('.');
                int iter = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] hashOriginal = Convert.FromBase64String(parts[2]);

                using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iter, HashAlgorithmName.SHA256);
                byte[] hashAtual = pbkdf2.GetBytes(32);

                return CryptographicOperations.FixedTimeEquals(hashOriginal, hashAtual);
            }
            catch
            {
                return false;
            }
        }
    }
}