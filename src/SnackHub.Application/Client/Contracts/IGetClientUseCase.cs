using SnackHub.Application.Client.Models;

namespace SnackHub.Application.Client.Contracts
{
    public interface IGetClientUseCase
    {
        Task<GetClientResponse?> Execute(Guid id);
        Task<GetClientResponse?> Execute(string cpf);
    }
}
