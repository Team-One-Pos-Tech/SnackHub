using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.UseCases
{
    public class GetClientUseCase(IClientRepository clientRepository) : IGetClientUseCase
    {
        public GetClientResponse Execute(Guid id)
        {
            var client = clientRepository.Get(id);

            return new GetClientResponse(client.Name, client.CPF);
        }
    }
}