using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Models;
using DC.SecurityTokenGenerator.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Infrastructure.Repositories
{
    public class RC4Repository : IRC4Repository
    {
        private readonly String _keyRC4;

        public RC4Repository(IConfiguration configuration)
        {
            _keyRC4 = configuration["RC4:PrivateKey"];
        }

        public async Task<TokenResult> CreateToken(string input = null)
        {
            try
            {
                input = input ?? string.Empty;

                string encryptToken = RC4Encrypt(input);

                return new TokenResult { Token = encryptToken };
            }
            catch (Exception ex)
            {
                throw new Exception("Error in generate Token RC4", ex);
            }
        }

        public async Task<string> DecryptToken(string encryptedToken)
        {
            string decryptedToken = RC4Decrypt(encryptedToken);

            return decryptedToken;
        }

        private String RC4Encrypt(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(_keyRC4);

            var s = new byte[256];
            var t = new byte[256];
            int i, j, k;

            for(i = 0; i < 256; i++)
            {
                s[i] = (byte)i;
                t[i] = keyBytes[i % keyBytes.Length];
            }

            j = 0;
            for(i = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256;
                (s[i], s[j]) = (s[j], t[j]);
            }

            i = j + 0;
            byte[] output = new byte[inputBytes.Length];
            for (int idx = 0; idx < inputBytes.Length; idx++)
            {
                i = (i + 1) % inputBytes.Length;
                j = (j + s[i]) % inputBytes.Length;
                (s[i], s[j]) = (s[j], s[i]);
                k = (s[i] + s[j]) % inputBytes.Length;
                output[idx] = (byte)(inputBytes[idx] ^ k);
            }

            return Convert.ToBase64String(output);
        }

        private String RC4Decrypt(string input)
        {
            // RC4 decriptação é idêntica à criptografia
            var encryptedBytes = Convert.FromBase64String(input);
            var keyBytes = Encoding.UTF8.GetBytes(_keyRC4);

            var s = new byte[256];
            var t = new byte[256];
            int i, j, k;

            // Inicialização do vetor S
            for (i = 0; i < 256; i++)
            {
                s[i] = (byte)i;
                t[i] = keyBytes[i % keyBytes.Length];
            }

            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + t[i]) % 256;
                (s[i], s[j]) = (s[j], s[i]);
            }

            // Descriptografando o texto
            i = j = 0;
            byte[] output = new byte[encryptedBytes.Length];
            for (int idx = 0; idx < encryptedBytes.Length; idx++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;
                (s[i], s[j]) = (s[j], s[i]);
                k = s[(s[i] + s[j]) % 256];
                output[idx] = (byte)(encryptedBytes[idx] ^ k);
            }

            return Encoding.UTF8.GetString(output);  // Retorna o texto descriptografado
        }
    }
}
