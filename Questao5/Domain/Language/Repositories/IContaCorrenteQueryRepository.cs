using Questao5.Domain.Entities;

namespace Questao5.Domain.Language.Repositories
{
    public interface IContaCorrenteQueryRepository : IBaseQueryRepository<ContaCorrente>
    {
        Task<ContaCorrente?> ConsultarAsync(long numero, CancellationToken cancellationToken);
        Task<double> ConsultarSaldoAsync(string id, CancellationToken cancellationToken);
    }
}
