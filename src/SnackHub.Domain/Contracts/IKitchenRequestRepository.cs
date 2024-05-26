using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts;

public interface IKitchenRequestRepository
{
    Task<IEnumerable<KitchenRequest>> ListAllAsync();
}