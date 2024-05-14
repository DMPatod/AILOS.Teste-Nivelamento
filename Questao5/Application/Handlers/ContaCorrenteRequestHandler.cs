using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Exceptions;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Application.Handlers
{
    public class ContaCorrenteRequestHandler : IRequestHandler<ContaCorrenteRequest, ContaCorrente>
    {
        private readonly IContaCorrenteQueryRepository queryRepository;

        public ContaCorrenteRequestHandler(IContaCorrenteQueryRepository queryRepository)
        {
            this.queryRepository = queryRepository;
        }

        public async Task<ContaCorrente> Handle(ContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await queryRepository.ConsultarAsync(request.numeroContaCorrente, cancellationToken);

            return contaCorrente is null ? throw new InvalidAccountException() : contaCorrente;
        }
    }
}
