using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB;

public class KitchenRequestRepository : BaseRepository<KitchenRequest>, IKitchenRequestRepository
{
    public KitchenRequestRepository(IMongoDatabase mongoDatabase, string collectionName = "KitchenRequest") 
        : base(mongoDatabase, collectionName)
    {
    }

    public async Task<KitchenRequest?> GetByOderIdAsync(Guid orderId)
    {
        return await FindByPredicateAsync(kitchenRequest => kitchenRequest.OrderId.Equals(orderId));
    }
    
    public async Task EditAsync(KitchenRequest kitchenRequest)
    {
        await UpdateByPredicateAsync(request => request.Id.Equals(kitchenRequest.Id), kitchenRequest);
    }

    public async Task<IEnumerable<KitchenRequest>> ListAllAsync()
    {
        return await ListByPredicateAsync(kitchenRequest => kitchenRequest.Id != Guid.Empty); // Todo: Add Better Filter, Possible by date
    }
}