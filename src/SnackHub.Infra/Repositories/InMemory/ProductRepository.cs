using SnackHub.Domain.Contracts;

namespace SnackHub.Infra.Repositories.InMemory;

public class ProductRepository : IProductRepository
{
    public Task<IEnumerable<(Guid Id, string Name, decimal Price)>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return Task.FromResult(Enumerable.Empty<(Guid Id, string Name, decimal Price)>());
    }
}