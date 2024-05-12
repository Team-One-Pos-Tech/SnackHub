using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.UseCases
{
    public class GetClientUseCase(IClientRepository clientRepository)
    {
        public GetClientResponse Execute(Guid id)
        {
            var client = clientRepository.Get(id);

            return new GetClientResponse(client.Name, client.CPF);
        }
    }
}