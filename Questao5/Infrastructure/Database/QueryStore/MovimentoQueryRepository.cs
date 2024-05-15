using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class MovimentoQueryRepository : IBaseQueryRepository<Movimento>
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoQueryRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task<Movimento?> ConsultarAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Movimento>> ConsultarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
