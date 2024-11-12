using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Enums;
using DC.SecurityTokenGenerator.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Domain.Command
{
    public class CreateTokenCommand : IRequest<BaseResponse<TokenResult>>
    {
        public ETokenType TokenType { get; set; }

        public CreateTokenCommand(ETokenType TokenType)
        {
            this.TokenType = TokenType;
        }

        public CreateTokenCommand(int tokenType)
        {
            this.TokenType = (ETokenType)tokenType;
        }
    }
}
