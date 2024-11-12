using DC.SecurityTokenGenerator.Domain.Command;
using DC.SecurityTokenGenerator.Domain.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DC.SecurityTokenGenerator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenGeneratorController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TokenGeneratorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> CreateToken([FromBody] GenerateTokenRequest request)
        {
            var result = await _mediator.Send(new CreateTokenCommand(request.TypeToken));

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
