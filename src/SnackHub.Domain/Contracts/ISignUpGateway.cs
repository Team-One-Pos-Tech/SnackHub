using SnackHub.Domain.Models;

namespace SnackHub.Domain.Contracts;

public interface ISignUpFunctionGateway
{
    Task Execute(SignUpRequest request);
}