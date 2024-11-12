using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Infrastructure.Repositories
{
    public class _3DESRepository : I3DESRepository
    {
        private readonly String _Key3DES;
        private readonly String _IV3DES;

        public _3DESRepository(IConfiguration configuration)
        {
            _Key3DES = configuration["DES3:PrivateKey"];
            _IV3DES = configuration["DES3:Iv"];
        }

        public async Task<TokenResult> CreateToken(string input = null)
        {
            try
            {
                input = input ?? String.Empty;

                String encryptedToken = default;
                using(TripleDES tripleDES = new TripleDESCryptoServiceProvider())
                {
                    byte[] keyBytes = Encoding.ASCII.GetBytes(_Key3DES);
                    byte[] ivBytes = Encoding.ASCII.GetBytes(_IV3DES);

                    tripleDES.Key = keyBytes;
                    tripleDES.IV = ivBytes;
                    tripleDES.Mode = CipherMode.CBC;
                    tripleDES.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform encryptor = tripleDES.CreateEncryptor())
                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(input);
                        sw.Flush();
                        sw.Close();
                        encryptedToken = Convert.ToBase64String(ms.ToArray());
                    }
                }

                return new TokenResult { Token = encryptedToken };
            }
            catch(Exception ex)
            {
                throw new Exception("Error in Generate Token 3DES", ex);
            }
        }

        public async Task<string> DecryptToken(string encryptedToken)
        {
            try
            {
                using (var tripleDES = new TripleDESCryptoServiceProvider())
                {
                    // Converte a chave e o vetor de inicialização para bytes
                    byte[] keyBytes = Encoding.UTF8.GetBytes(_Key3DES);
                    byte[] ivBytes = Convert.FromBase64String(_IV3DES);

                    // Configura a chave e o IV para a descriptografia
                    tripleDES.Key = keyBytes;
                    tripleDES.IV = ivBytes;
                    tripleDES.Mode = CipherMode.CBC;  // Modo CBC (Cipher Block Chaining)
                    tripleDES.Padding = PaddingMode.PKCS7;  // Preenchimento PKCS7

                    // Descriptografa os dados
                    using (ICryptoTransform decryptor = tripleDES.CreateDecryptor())
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedToken)))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();  // Retorna o texto original
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Decrypt Token 3DES", ex);
            }
        }
    }
}
