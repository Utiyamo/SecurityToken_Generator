using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Core.Handlers
{
    public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, BaseResponse<TokenResult>>
    {
        private readonly TokenRepository _AESTokenRepository;

        public CreateTokenHandler(AES aesTokenRepository)
        {
            _AESTokenRepository = aesTokenRepository;
        }

        public async Task<BaseResponse<TokenResult>> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
