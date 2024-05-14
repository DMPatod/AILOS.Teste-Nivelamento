using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public record CadastrarMovimentacaoRequest(long NumeroContaCorrente, double ValorMovimentacao, TipoMovimento TipoMovimento) : IRequest<CadastrarMovimentacaoResponse>;
}
