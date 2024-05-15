using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IGetClientUseCase
    {
        GetClientResponse Execute(Guid id);
    }
}
