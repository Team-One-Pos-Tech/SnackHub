using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts;

public interface IKitchenRequestRepository
{
    Task<KitchenRequest?> GetByOderIdAsync(Guid orderId);
    Task EditAsync(KitchenRequest kitchenRequest);
    Task<IEnumerable<KitchenRequest>> ListAllAsync();
}