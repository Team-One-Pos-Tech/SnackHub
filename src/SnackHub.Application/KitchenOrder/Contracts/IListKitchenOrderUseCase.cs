using SnackHub.Application.KitchenOrder.Models;

namespace SnackHub.Application.KitchenOrder.Contracts;

public interface IListKitchenOrderUseCase
{
    Task<IEnumerable<KitchenOrderResponse>> Execute();
}