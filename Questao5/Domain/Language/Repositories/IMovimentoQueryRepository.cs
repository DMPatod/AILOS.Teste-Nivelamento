using Questao5.Domain.Entities;

namespace Questao5.Domain.Language.Repositories
{
    public interface IMovimentoQueryRepository : IBaseQueryRepository<Movimento>
    {
        Task<IList<Movimento>> ConsultarPorContaCorrenteAsync(string IdContaCorrente, CancellationToken cancellationToken);
    }
}
