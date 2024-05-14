using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Application.Handlers
{
    public class CadastrarMovimentacaoRequestHandler : IRequestHandler<CadastrarMovimentacaoRequest, CadastrarMovimentacaoResponse>
    {
        private readonly IMediator mediator;
        private readonly IBaseCommandRepository<Idempotencia> commandRepository;
        private readonly string messageSuccesso = "Transação processada com sucesso";

        public CadastrarMovimentacaoRequestHandler(IMediator mediator, IBaseCommandRepository<Idempotencia> commandRepository)
        {
            this.mediator = mediator;
            this.commandRepository = commandRepository;
        }

        public async Task<CadastrarMovimentacaoResponse> Handle(CadastrarMovimentacaoRequest request, CancellationToken cancellationToken)
        {
            Idempotencia idempotencia;
            try
            {
                var contaCorrenteRequest = new ContaCorrenteRequest(request.NumeroContaCorrente);
                var contaCorrente = await mediator.Send(contaCorrenteRequest, cancellationToken);

                var consultarSaldoContaCorrenteRequest = new ConsultarSaldoContaCorrenteRequest(contaCorrente.Numero);
                var saldoContaCorrente = await mediator.Send(consultarSaldoContaCorrenteRequest, cancellationToken);

                ValidarOperacao(saldoContaCorrente.Valor, request.ValorMovimentacao, request.TipoMovimento);

                idempotencia = Idempotencia.Criar(request, true, messageSuccesso);
            }
            catch (Exception ex)
            {
                idempotencia = Idempotencia.Criar(request, false, ex.Message);
            }

            await commandRepository.CadastrarAsync(idempotencia, cancellationToken);

            return new CadastrarMovimentacaoResponse
            (
                idempotencia.Chave_Idempotencia,
                idempotencia.Requisicao,
                idempotencia.Resultado
            );
        }

        private void ValidarOperacao(double saldo, double movimento, TipoMovimento tipoMovimento)
        {
            if (tipoMovimento == TipoMovimento.Debito)
            {
                if (saldo < movimento)
                {
                    throw new InvalidValueException();
                }
            }
        }
    }
}
