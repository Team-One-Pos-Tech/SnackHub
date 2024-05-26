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

    public async Task<IEnumerable<KitchenRequest>> ListAllAsync()
    {
        return await ListByPredicateAsync(px => px.Id != Guid.Empty); // Todo: Add Better Filter, Possible by date
    }
}