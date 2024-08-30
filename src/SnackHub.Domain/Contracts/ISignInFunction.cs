using SnackHub.Domain.Models.Gateways;
using SnackHub.Domain.Models.Gateways.Models;

namespace SnackHub.Domain.Contracts;

public interface ISignInFunctionGateway
{
    public Task<AuthResponseType> Execute(SignUpRequest request);
}