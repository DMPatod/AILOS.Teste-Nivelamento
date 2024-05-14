using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Application.Handlers
{
    public class IdempotenciaRequestHandler : IRequestHandler<IdempotenciaRequest, Idempotencia>
    {
        private readonly IBaseQueryRepository<Idempotencia> queryRepository;

        public IdempotenciaRequestHandler(IBaseQueryRepository<Idempotencia> queryRepository)
        {
            this.queryRepository = queryRepository;
        }

        public async Task<Idempotencia> Handle(IdempotenciaRequest request, CancellationToken cancellationToken)
        {
            var idempotencia = await queryRepository.ConsultarAsync(request.Id, cancellationToken);
            return idempotencia is null ? throw new Exception() : idempotencia;
        }
    }
}
