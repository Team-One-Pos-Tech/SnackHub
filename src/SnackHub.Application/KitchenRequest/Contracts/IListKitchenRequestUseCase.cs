using SnackHub.Application.KitchenRequest.Models;

namespace SnackHub.Application.KitchenRequest.Contracts;

public interface IListKitchenRequestUseCase
{
    Task<IEnumerable<KitchenRequestResponse>> Execute();
}