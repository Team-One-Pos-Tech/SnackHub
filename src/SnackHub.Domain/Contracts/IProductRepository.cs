using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task EditAsync(Product product);
        Task RemoveAsync(Guid id);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> ListAllAsync();
    }
}
