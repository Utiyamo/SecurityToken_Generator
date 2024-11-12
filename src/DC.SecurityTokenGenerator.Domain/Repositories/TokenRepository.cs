using DC.SecurityTokenGenerator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Domain.Repositories
{
    public interface TokenRepository
    {
        Task<TokenResult> CreateToken(string input = null);
        Task<String> DecryptToken(string encryptedToken);
    }
}
