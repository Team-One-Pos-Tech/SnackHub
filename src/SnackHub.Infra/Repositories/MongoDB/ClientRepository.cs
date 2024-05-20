using MongoDB.Driver;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

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

    public async Task<Client?> GetClientByIdAsync(Guid id)
    {
        return await FindByPredicateAsync(px => px.Id.Equals(id));
    }

    public async Task<Client?> GetClientByCpfAsync(CPF cpf)
    {
        return await FindByPredicateAsync(px => px.CPF.Equals(cpf));
    }
    
    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await ExistsByPredicateAsync(px => px.Id.Equals(id));
    }
}