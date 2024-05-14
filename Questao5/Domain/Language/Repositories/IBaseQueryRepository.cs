namespace Questao5.Domain.Language.Repositories
{
    public interface IBaseQueryRepository<T>
    {
        Task<T?> ConsultarAsync(string id, CancellationToken cancellationToken);
        Task<IList<T>> ConsultarAsync(CancellationToken cancellationToken);
    }
}
