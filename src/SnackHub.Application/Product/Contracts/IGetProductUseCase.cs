using SnackHub.Application.Models;

namespace SnackHub.Application.Contracts
{
    public interface IGetProductUseCase
    {
        Task<GetProductResponse?> Execute(Guid id);
        Task<IEnumerable<GetProductResponse>> Execute();
    }
}
