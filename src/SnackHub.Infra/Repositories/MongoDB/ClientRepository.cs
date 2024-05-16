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
    
    public void Add(Client client)
    {
        MongoCollection.InsertOne(client);
    }

    public Client Get(Guid id)
    {
        return MongoCollection
            .Find(px => px.Id.Equals(id))
            .FirstOrDefault();
    }
}