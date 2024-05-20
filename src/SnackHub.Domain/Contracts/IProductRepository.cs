using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<IEnumerable<Product>> GetByCategoriaAsync(Category category);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}
