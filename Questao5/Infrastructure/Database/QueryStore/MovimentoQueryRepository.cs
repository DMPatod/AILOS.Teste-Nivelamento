using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Language.Repositories;
using Questao5.Infrastructure.Sqlite;
using System.Globalization;

namespace Questao5.Infrastructure.Database.QueryStore
{
    public class MovimentoQueryRepository : IMovimentoQueryRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public MovimentoQueryRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public Task<Movimento> ConsultarAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Movimento>> ConsultarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Movimento>> ConsultarPorContaCorrenteAsync(string idContaCorrente, CancellationToken cancellationToken)
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var sql = $@"
SELECT 
    m.idmovimento AS Id,
    m.idcontacorrente AS IdContaCorrente,
    m.datamovimento AS DataMovimento,
    m.tipomovimento AS TipoMovimento,
    m.valor AS Valor
FROM movimento m
WHERE idcontacorrente = '{idContaCorrente}'
";

            var query = await connection.QueryAsync(sql, (Func<dynamic, Movimento>)(row =>
            {
                return new Movimento(
                    row.Id,
                    row.IdContaCorrente,
                    DateTime.ParseExact(row.DataMovimento, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    row.TipoMovimento switch
                    {
                        'C' => TipoMovimento.Credito,
                        'D' => TipoMovimento.Debito,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    row.Valor);
            }));

            return query.ToList();
        }
    }
}
