using DC.SecurityTokenGenerator.Domain.Command;
using DC.SecurityTokenGenerator.Domain.Entities;
using DC.SecurityTokenGenerator.Domain.Models;
using DC.SecurityTokenGenerator.Domain.Repositories;
using MediatR;

namespace DC.SecurityTokenGenerator.Domain.Handler
{
    public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, BaseResponse<TokenResult>>
    {
        private readonly IAESRepository _AESTokenRepository;
        private readonly IRC4Repository _RC4Repository;
        private readonly I3DESRepository _DESRepository;
        private readonly IIDEARepository _IDEARepository;

        public CreateTokenHandler(IAESRepository aesTokenRepository, IRC4Repository rc4Repository, I3DESRepository des3Repository, IIDEARepository iDEARepository) 
        {
            _AESTokenRepository = aesTokenRepository;
            _RC4Repository = rc4Repository;
            _DESRepository = des3Repository;    
            _IDEARepository = iDEARepository;
        }

        public async Task<BaseResponse<TokenResult>> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
        {
            try
            {
                TokenResult token;
                switch (command.TokenType)
                {
                    case Enums.ETokenType.AES:
                        token = await _AESTokenRepository.CreateToken(Guid.NewGuid().ToString());
                        break;

                    case Enums.ETokenType.RC4:
                        token = await _RC4Repository.CreateToken(Guid.NewGuid().ToString());
                        break;

                    case Enums.ETokenType.DES3:
                        token = await _DESRepository.CreateToken(Guid.NewGuid().ToString());
                        break;

                    case Enums.ETokenType.IDEA:
                        token = await _IDEARepository.CreateToken(Guid.NewGuid().ToString());
                        break;

                    default:
                        throw new Exception("Token Type not mapped on system");
                }

                return BaseResponse<TokenResult>.Success(token);    
            }
            catch (Exception ex)
            {
                return BaseResponse<TokenResult>.Error(ex.Message);
            }
        }
    }
}
