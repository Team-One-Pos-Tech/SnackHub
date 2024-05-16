using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);

        Task<Client> GetClientByIdAsync(Guid id);
    }
}