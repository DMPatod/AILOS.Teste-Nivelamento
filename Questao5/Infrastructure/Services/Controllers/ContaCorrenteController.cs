using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{numeroContaCorrente}/Saldo")]
        public async Task<IActionResult> ConsultarSaldo([FromRoute] long numeroContaCorrente)
        {
            var request = new ConsultarSaldoContaCorrenteRequest(numeroContaCorrente);
            var response = await mediator.Send(request, CancellationToken.None);
            return Ok(response);
        }
    }
}
