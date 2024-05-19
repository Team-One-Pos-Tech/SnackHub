using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.InMemory;

public class ProductRepository : IProductRepository
{
    public Task<Product> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Product>> IProductRepository.GetByIdsAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetByCategoriaAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}