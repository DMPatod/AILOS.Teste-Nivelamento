namespace Questao5.Domain.Language.Repositories
{
    public interface IBaseCommandRepository<T>
    {
        Task CadastrarAsync(T entidade, CancellationToken cancellationToken);
        Task AtualizarAsync(string id, T entidade, CancellationToken cancellationToken);
    }
}
