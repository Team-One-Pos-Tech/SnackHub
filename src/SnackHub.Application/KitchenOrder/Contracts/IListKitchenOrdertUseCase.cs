using SnackHub.Application.KitchenOrder.Models;

namespace SnackHub.Application.KitchenOrder.Contracts;

public interface IListKitchenOrdertUseCase
{
    Task<IEnumerable<KitchenOrderResponse>> Execute();
}