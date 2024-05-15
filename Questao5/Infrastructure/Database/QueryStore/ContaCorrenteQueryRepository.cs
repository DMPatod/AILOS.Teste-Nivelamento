using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class ContaCorrenteQueryRepository : IContaCorrenteQueryRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContaCorrenteQueryRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task<ContaCorrente?> ConsultarAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ContaCorrente>> ConsultarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ContaCorrente?> ConsultarAsync(long numero, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = @"
SELECT
    c.idcontacorrente AS Id,
    c.numero AS Numero,
    c.nome AS Titular,
    c.ativo AS Ativo
FROM contacorrente c
WHERE numero = @numero
";
            var paramerters = new
            {
                numero
            };
            var query = await connection.QueryAsync<ContaCorrente>(sql, paramerters);
            return query.FirstOrDefault();
        }

        public async Task<double> ConsultarSaldoAsync(string id, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = @"
SELECT
    SUM(CASE WHEN tipomovimento = 'C' THEN valor ELSE 0 END) - SUM(CASE WHEN tipomovimento = 'D' THEN valor ELSE 0 END)
FROM
    movimento
WHERE
    idcontacorrente = @id
";
            var parameters = new
            {
                id
            };
            var query = await connection.QuerySingleAsync<double>(sql, parameters);
            return query;
        }
    }
}
