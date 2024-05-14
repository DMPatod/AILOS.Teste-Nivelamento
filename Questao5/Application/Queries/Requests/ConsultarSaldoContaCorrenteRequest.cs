using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public record ConsultarSaldoContaCorrenteRequest(long NumeroContaCorrente) : IRequest<ConsultarSaldoContaCorrenteResponse>;
}
