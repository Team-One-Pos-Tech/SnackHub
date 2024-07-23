using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Infra.Repositories.MongoDB;

public class KitchenOrderRepository : BaseRepository<KitchenOrder>, IKitchenOrderRepository
{
    private readonly ILogger<KitchenOrderRepository> _logger;
    
    public KitchenOrderRepository(
        ILogger<KitchenOrderRepository> logger,
        IMongoDatabase mongoDatabase,
        string collectionName = "KitchenRequest") 
        : base(mongoDatabase, collectionName)
    {
        _logger = logger;
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
        var query = MongoCollection.AsQueryable()
            .OrderBy(ko => ko.CreatedAt);
        
        _logger.LogDebug("MongoDB query: {Query}", query);
        
        var result = query.ToList();
        
        return await Task.FromResult(result);
    }
    
    public async Task<IEnumerable<KitchenOrder>> ListCurrentAsync()
    {
        var query = MongoCollection.AsQueryable()
            .Where(ko => ko.Status != KitchenOrderStatus.Finished)
            .OrderByDescending(ko => (int)ko.Status)
            .ThenBy(ko => ko.CreatedAt);

        _logger.LogDebug("MongoDB query: {Query}", query);
        
        var result = query.ToList();
        
        return await Task.FromResult(result);
    }
}