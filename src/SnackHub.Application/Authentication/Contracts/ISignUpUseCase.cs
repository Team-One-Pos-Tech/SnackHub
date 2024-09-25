using SnackHub.Application.Client.Models;
using SnackHub.Domain.Models.Gateways;

namespace SnackHub.Application.Authentication.Contracts
{
    public interface ISignUpUseCase
    {
        Task<RegisterClientResponse> Execute(SignUpRequest request);
    }
}