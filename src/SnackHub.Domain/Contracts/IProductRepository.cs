namespace SnackHub.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<(Guid Id, string Name, decimal Price)>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}