using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IRegisterClientUseCase
    {
        Task<RegisterClientResponse> Execute(RegisterClientRequest registerClientRequest);
    }
}