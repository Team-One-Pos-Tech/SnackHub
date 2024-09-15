using SnackHub.Application.Authentication.Models;
using SnackHub.Domain.Models.Gateways;

namespace SnackHub.Application.Authentication.Contracts
{
    public interface ISignInUseCase
    {
        Task<SignInResponse> Execute(SignInRequest request);
    }
}