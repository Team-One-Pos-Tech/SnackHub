using SnackHub.Domain.Entities;

namespace SnackHub.Application.Adapters
{
    public interface IClientRepository
    {
        void Add(Client client);
    }
}