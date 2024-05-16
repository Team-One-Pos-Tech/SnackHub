using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IGetClientUseCase
    {
        Task<GetClientResponse> Execute(Guid id);
    }
}
