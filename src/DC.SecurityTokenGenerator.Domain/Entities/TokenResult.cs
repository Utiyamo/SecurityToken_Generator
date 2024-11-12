using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Domain.Entities
{
    public class TokenResult
    {
        public String Token { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
