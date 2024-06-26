using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB;

public sealed class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(IMongoDatabase mongoDatabase, string collectionName = "Orders") 
        : base(mongoDatabase, collectionName)
    {
    }
    
    public async Task AddAsync(Order order)
    {
        await InsertAsync(order);
    }
    
    public async Task EditAsync(Order order)
    {
        await UpdateByPredicateAsync(x => x.Id.Equals(order.Id), order);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await FindByPredicateAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<Order>> ListAllAsync()
    {
        return await ListByPredicateAsync(order => order.Id != Guid.Empty); // Todo: Add Better Filter, Possible by date
    }
}