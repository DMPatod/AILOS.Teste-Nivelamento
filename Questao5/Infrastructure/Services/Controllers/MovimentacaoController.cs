using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMediator mediator;

        public MovimentacaoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarMovimentacao([FromBody] CadastrarMovimentacaoRequest request)
        {
            var response = await mediator.Send(request, CancellationToken.None);
            return Ok(response);
        }

        [HttpPost("{idIdempotencia}")]
        public async Task<IActionResult> RealizarMovimentacao([FromRoute] string idIdempotencia, [FromBody] CadastrarMovimentacaoRequest contract)
        {
            var request = new MovimentarContaCorrenteRequest(idIdempotencia, contract.NumeroContaCorrente, contract.ValorMovimentacao, contract.TipoMovimento);
            var response = await mediator.Send(request, CancellationToken.None);
            return Ok(response);
        }
    }
}
