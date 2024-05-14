using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class ContaCorrenteCommandRepository : IBaseCommandRepository<ContaCorrente>
    {
        public Task AtualizarAsync(string id, ContaCorrente entidade, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task CadastrarAsync(ContaCorrente entidade, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
