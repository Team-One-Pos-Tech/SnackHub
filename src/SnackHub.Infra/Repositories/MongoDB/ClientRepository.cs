using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB;

public sealed class ClientRepository : BaseRepository<Client>, IClientRepository
{
    public ClientRepository(IMongoDatabase mongoDatabase, string collectionName = "Clients") 
        : base(mongoDatabase, collectionName)
    {
    }
    
    public async Task AddAsync(Client client)
    {
        await InsertAsync(client);
    }

    public async Task<Client> GetClientByIdAsync(Guid id)
    {
        return await FindByPredicateAsync(px => px.Id.Equals(id));
        
    }
}