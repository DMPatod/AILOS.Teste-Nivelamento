using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class IdempotenciaQueryRepository : IBaseQueryRepository<Idempotencia>
    {
        private readonly DatabaseConfig databaseConfig;

        public IdempotenciaQueryRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        async Task<Idempotencia?> IBaseQueryRepository<Idempotencia>.ConsultarAsync(string id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = @"
SELECT *
FROM idempotencia
WHERE chave_idempotencia = @id";

            var parameters = new
            {
                id
            };
            var query = await connection.QueryAsync<Idempotencia>(sql, parameters);
            return query.FirstOrDefault();
        }

        Task<IList<Idempotencia>> IBaseQueryRepository<Idempotencia>.ConsultarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
