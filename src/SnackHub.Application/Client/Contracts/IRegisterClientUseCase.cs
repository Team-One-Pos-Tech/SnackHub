using SnackHub.Application.Client.Models;

namespace SnackHub.Application.Client.Contracts
{
    public interface IRegisterClientUseCase
    {
        Task<RegisterClientResponse> Execute(RegisterClientRequest registerClientRequest);
    }
}