using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB;

public class KitchenOrderRepository : BaseRepository<KitchenOrder>, IKitchenOrderRepository
{
    public KitchenOrderRepository(IMongoDatabase mongoDatabase, string collectionName = "KitchenRequest") 
        : base(mongoDatabase, collectionName)
    {
    }

    public async Task<KitchenOrder?> GetByOderIdAsync(Guid orderId)
    {
        return await FindByPredicateAsync(kitchenRequest => kitchenRequest.OrderId.Equals(orderId));
    }

    public async Task AddAsync(KitchenOrder kitchenOrder)
    {
        await InsertAsync(kitchenOrder);
    }

    public async Task EditAsync(KitchenOrder kitchenOrder)
    {
        await UpdateByPredicateAsync(request => request.Id.Equals(kitchenOrder.Id), kitchenOrder);
    }

    public async Task<IEnumerable<KitchenOrder>> ListAllAsync()
    {
        return await ListByPredicateAsync(kitchenRequest => kitchenRequest.Id != Guid.Empty); // Todo: Add Better Filter, Possible by date
    }
}