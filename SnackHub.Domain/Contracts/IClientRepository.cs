using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts
{
    public interface IClientRepository
    {
        void Add(Client client);

        Client Get(Guid id);
    }
}