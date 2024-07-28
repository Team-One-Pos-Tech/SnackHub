using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Application.Client.Validators
{
    public class RegisterClientValidator(IClientRepository _clientRepository) : IRegisterClientValidator
    {
        public async Task<bool> IsValid(RegisterClientRequest registerClientRequest, RegisterClientResponse response)
        {
            var cpf = new CPF(registerClientRequest.CPF);

            if (!cpf.IsValid())
            {
                response.AddNotification("CPF", "CPF is invalid.");
                return false;
            }

            var existsCpf = await _clientRepository.GetClientByCpfAsync(cpf);
            if (existsCpf != null)
            {
                response.AddNotification("CPF", "CPF is already registered.");
                return false;
            }

            return true;
        }
    }
}