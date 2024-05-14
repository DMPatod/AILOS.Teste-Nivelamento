using MediatR;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public record MovimentarContaCorrenteRequest(string IdMovimentacao, long NumeroContaCorrente, double ValorMovimentacao, TipoMovimento TipoMovimento) : IRequest<Movimento>;
}
