using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Domain.Enums
{
    public enum ETokenType
    {
        AES = 1,   
        RC4,  
        DES3,  
        IDEA
    }
}
