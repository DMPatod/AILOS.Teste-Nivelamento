using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Requests
{
    public record ContaCorrenteRequest(long numeroContaCorrente) : IRequest<ContaCorrente>;
}
