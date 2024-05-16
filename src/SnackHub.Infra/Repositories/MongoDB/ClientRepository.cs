using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub.Infra.Repositories.MongoDB;

public class ClientRepository : IClientRepository
{
    private readonly IMongoCollection<Client> _mongoCollection;

    public ClientRepository(IMongoDatabase mongoDatabase, string collectionName = "Clients")
    {
        if (mongoDatabase is null)
            throw new ArgumentException("MongoDatabase could not be null");

        _mongoCollection =
            mongoDatabase.GetCollection<Client>(collectionName == string.Empty ? nameof(Client) : collectionName);
    }
    
    public void Add(Client client)
    {
        _mongoCollection.InsertOne(client);
    }

    public Client Get(Guid id)
    {
        return _mongoCollection
            .Find(px => px.Id.Equals(id))
            .FirstOrDefault();
    }
}