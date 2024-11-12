using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Infrastructure.Repositories
{
    public class AESRepository : IAESRepository
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public AESRepository()
        {
            // Gerar uma chave de 256 bits (32 bytes) para AES
            key = new byte[32]; // 32 bytes = 256 bits
            new RNGCryptoServiceProvider().GetBytes(key);

            // Gerar um IV de 128 bits (16 bytes) para AES
            iv = new byte[16]; // 16 bytes = 128 bits
            new RNGCryptoServiceProvider().GetBytes(iv);
        }

        public async Task<TokenResult> CreateToken(string input = null)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Criptografar os dados
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Converter o texto de entrada para bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // Criar um fluxo de memória para armazenar os bytes criptografados
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(inputBytes, 0, inputBytes.Length);
                    }

                    // Obter o vetor de bytes criptografado
                    byte[] encryptedBytes = ms.ToArray();

                    // Retornar o token criptografado em base64 para facilitar o armazenamento
                    return new TokenResult
                    {
                        Token = Convert.ToBase64String(encryptedBytes),
                        Expiration = null
                    };
                }
            }
        }
    
        public async Task<String> DecryptToken(string encryptedToken)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedToken);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Criar o descriptografador
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    // Ler o texto descriptografado
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
