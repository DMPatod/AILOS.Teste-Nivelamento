using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore
{
    public class MovimentoCommandRepository : IBaseCommandRepository<Movimento>
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoCommandRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task AtualizarAsync(string id, Movimento entidade, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task CadastrarAsync(Movimento entidade, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = @"
INSERT INTO movimento
(
    idmovimento,
    idcontacorrente,
    datamovimento,
    tipomovimento,
    valor
)
VALUES
(
    @id,
    @idContaCorrente,
    @dataMovimento,
    @tipoMovimento,
    @valor
)
";
            var parameters = new
            {
                id = entidade.Id,
                idContaCorrente = entidade.IdContaCorrente,
                dataMovimento = entidade.DataMovimento.ToString("dd/MM/yyyy"),
                tipoMovimento = entidade.TipoMovimento switch
                {
                    TipoMovimento.Credito => 'C',
                    TipoMovimento.Debito => 'D',
                    _ => throw new NotImplementedException(),
                },
                valor = entidade.Valor
            };

            await connection.ExecuteAsync(sql, parameters);
        }
    }
}
