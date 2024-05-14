using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaCorrenteRequestHandler : IRequestHandler<MovimentarContaCorrenteRequest, Movimento>
    {
        private readonly IMediator mediator;
        private readonly IBaseCommandRepository<Movimento> commandRepository;

        public MovimentarContaCorrenteRequestHandler(IMediator mediator, IBaseCommandRepository<Movimento> commandRepository)
        {
            this.mediator = mediator;
            this.commandRepository = commandRepository;
        }

        public async Task<Movimento> Handle(MovimentarContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var idempotenciaRequest = new IdempotenciaRequest(request.IdMovimentacao);
            var idempotencia = await mediator.Send(idempotenciaRequest, cancellationToken);

            var recordedRequest = JsonConvert.DeserializeObject<MovimentarContaCorrenteRequest>(idempotencia.Requisicao) ?? throw new Exception();
            if (request.ValorMovimentacao != recordedRequest.ValorMovimentacao
                || request.NumeroContaCorrente != recordedRequest.NumeroContaCorrente
                || request.TipoMovimento != recordedRequest.TipoMovimento)
            {
                throw new Exception();
            }

            var contaCorrenteRequest = new ContaCorrenteRequest(request.NumeroContaCorrente);
            var contaCorrente = await mediator.Send(contaCorrenteRequest, cancellationToken);

            var movimento = Movimento.Criar(
                idempotencia.Chave_Idempotencia,
                contaCorrente.Id,
                DateTime.Now,
                request.TipoMovimento,
                request.ValorMovimentacao);
            await commandRepository.CadastrarAsync(movimento, cancellationToken);

            return movimento;
        }
    }
}
