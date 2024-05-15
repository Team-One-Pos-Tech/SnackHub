using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IRegisterClientUseCase
    {
        RegisterClientResponse Execute(RegisterClientRequest registerClientRequest);
    }
}