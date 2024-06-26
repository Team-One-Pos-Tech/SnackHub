using System.Linq.Expressions;

namespace SnackHub.Infra.Repositories.Abstractions;

public interface IBaseRepository<TModel> where TModel : class
{
    Task InsertAsync(TModel model);
    Task DeleteByPredicateAsync(Expression<Func<TModel, bool>> predicate);

    Task<IEnumerable<TModel>> ListByPredicateAsync(Expression<Func<TModel, bool>> predicate);
    Task<TModel> FindByPredicateAsync(Expression<Func<TModel, bool>> predicate);

    Task<(int pageNumber, int pageSize, int lastPage, IEnumerable<TModel>)> ListPaginateAsync(
        Expression<Func<TModel, bool>> predicate, int pageNumber, int pageSize);
}