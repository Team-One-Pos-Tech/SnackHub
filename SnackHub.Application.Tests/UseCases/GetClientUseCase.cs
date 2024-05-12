
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.Tests.UseCases
{
    internal class GetClientUseCase(IClientRepository clientRepository)
    {
        internal GetClientResponse Execute(Guid id)
        {
            var client = clientRepository.Get(id);

            return new GetClientResponse(client.Name, client.CPF);
        }
    }
}