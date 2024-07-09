using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

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
        await UpdateByPredicateAsync(request => request.OrderId.Equals(kitchenOrder.OrderId), kitchenOrder);
    }

    public async Task<IEnumerable<KitchenOrder>> ListAllAsync()
    {
        var orders = await ListByPredicateAsync(_ => true);
        return orders.OrderBy(o => o.CreatedAt);
    }
    
    public async Task<IEnumerable<KitchenOrder>> ListCurrentAsync()
    {
        var orders = await ListByPredicateAsync(ko => ko.Status != KitchenOrderStatus.Finished);
        return orders
            .OrderByDescending(ko => ko.Status)
            .ThenBy(ko => ko.CreatedAt);
    }
}