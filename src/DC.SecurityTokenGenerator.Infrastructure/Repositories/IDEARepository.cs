using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Infrastructure.Repositories
{
    public class IDEARepository : IIDEARepository
    {
        private readonly String _KeyIDEA;

        public IDEARepository(IConfiguration configuration)
        {
            _KeyIDEA = configuration["IDEA:PrivateKey"];
        }

        public async Task<TokenResult> CreateToken(string input = null)
        {
            try
            {
                input = input ?? string.Empty;

                byte[] dataBytes = Encoding.UTF8.GetBytes(input.Substring(0, 16));
                byte[] keyBytes = Encoding.ASCII.GetBytes(_KeyIDEA);

                IdeaEngine ideaEngine = new IdeaEngine();
                BufferedBlockCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(ideaEngine));
                cipher.Init(true, new KeyParameter(keyBytes));

                byte[] encryptedBytes = cipher.DoFinal(dataBytes);

                return new TokenResult { Token = Convert.ToBase64String(encryptedBytes) };
            }
            catch(Exception ex)
            {
                throw new Exception("Error in Generation token IDEA", ex);
            }
        }

        public async Task<string> DecryptToken(string encryptedToken)
        {
            try
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedToken);
                byte[] chaveBytes = Encoding.UTF8.GetBytes(_KeyIDEA);

                // Inicializando o algoritmo IDEA para descriptografar
                IdeaEngine ideaEngine = new IdeaEngine();
                BufferedBlockCipher cipher = new BufferedBlockCipher(new CbcBlockCipher(ideaEngine));
                cipher.Init(false, new KeyParameter(chaveBytes));

                // Descriptografando os dados
                byte[] decryptedBytes = cipher.DoFinal(encryptedBytes);

                // Retorna o texto descriptografado como uma string
                return Encoding.UTF8.GetString(decryptedBytes).TrimEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Decrypt token IDEA", ex);
            }
        }
    }
}
