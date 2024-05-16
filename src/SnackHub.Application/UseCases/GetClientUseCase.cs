using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.UseCases
{
    public class GetClientUseCase(IClientRepository clientRepository) : IGetClientUseCase
    {
        public async Task<GetClientResponse> Execute(Guid id)
        {
            var client = await clientRepository.GetClientByIdAsync(id);
            return new GetClientResponse(client.Name, client.CPF);
        }
    }
}