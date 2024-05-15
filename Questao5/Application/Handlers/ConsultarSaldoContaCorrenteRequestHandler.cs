using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoContaCorrenteRequestHandler : IRequestHandler<ConsultarSaldoContaCorrenteRequest, ConsultarSaldoContaCorrenteResponse>
    {
        private readonly IMediator mediator;
        private readonly IContaCorrenteQueryRepository queryRepository;

        public ConsultarSaldoContaCorrenteRequestHandler(IMediator mediator, IContaCorrenteQueryRepository queryRepository)
        {
            this.mediator = mediator;
            this.queryRepository = queryRepository;
        }

        public async Task<ConsultarSaldoContaCorrenteResponse> Handle(ConsultarSaldoContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var contaCorrenteRequest = new ContaCorrenteRequest(request.NumeroContaCorrente);
            var contaCorrente = await mediator.Send(contaCorrenteRequest, cancellationToken) ?? throw new InvalidAccountException();

            if (!contaCorrente.Ativo)
            {
                throw new InactiveAccountException();
            }

            var saldo = await queryRepository.ConsultarSaldoAsync(contaCorrente.Id, cancellationToken);

            return new ConsultarSaldoContaCorrenteResponse(saldo);
        }
    }
}
