using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class IdempotenciaCommandRepository : IBaseCommandRepository<Idempotencia>
    {
        private readonly DatabaseConfig databaseConfig;

        public IdempotenciaCommandRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task AtualizarAsync(string id, Idempotencia entidade, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task CadastrarAsync(Idempotencia entidade, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = @"
INSERT INTO idempotencia(chave_idempotencia, requisicao, resultado) VALUES(@chave_idempotencia, @requisicao, @resultado)
";
            var parameters = new
            {
                chave_idempotencia = entidade.Chave_Idempotencia,
                requisicao = entidade.Requisicao,
                resultado = entidade.Resultado,
            };
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}
