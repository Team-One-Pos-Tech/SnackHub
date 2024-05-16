using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Client.UseCases
{
    public class GetClientUseCase(IClientRepository clientRepository) : IGetClientUseCase
    {
        public async Task<GetClientResponse?> Execute(Guid id)
        {
            var client = await clientRepository.GetClientByIdAsync(id);
            return client is null 
                ? null 
                : new GetClientResponse(client.Name, client.CPF);
        }

        public async Task<GetClientResponse?> Execute(string cpf)
        {
            var cpfObject = new CPF(cpf);
            if (!cpfObject.IsValid())
                return null; //Todo: Improve those validations!
            
            var client = await clientRepository.GetClientByCpfAsync(cpfObject);
            return client is null 
                ? null 
                : new GetClientResponse(client.Name, client.CPF);
        }
    }
}