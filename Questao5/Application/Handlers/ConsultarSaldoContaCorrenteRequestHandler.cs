using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoContaCorrenteRequestHandler : IRequestHandler<ConsultarSaldoContaCorrenteRequest, ConsultarSaldoContaCorrenteResponse>
    {
        private readonly IMediator mediator;
        private readonly IMovimentoQueryRepository queryRepository;

        public ConsultarSaldoContaCorrenteRequestHandler(IMediator mediator, IMovimentoQueryRepository queryRepository)
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

            var movimentacoes = await queryRepository.ConsultarPorContaCorrenteAsync(contaCorrente.Id, cancellationToken);
            var creditos = movimentacoes.Where(x => x.TipoMovimento == TipoMovimento.Credito).Sum(x => x.Valor);
            var debitos = movimentacoes.Where(x => x.TipoMovimento == TipoMovimento.Debito).Sum(x => x.Valor);

            return new ConsultarSaldoContaCorrenteResponse(creditos - debitos);
        }
    }
}
