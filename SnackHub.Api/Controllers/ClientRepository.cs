using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;

namespace SnackHub_Poc.Controllers
{
    public class ClientRepository : IClientRepository
    {
        public ClientRepository() 
        {
        }

        public void Add(Client client)
        {
            throw new NotImplementedException();
        }

        public Client Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}