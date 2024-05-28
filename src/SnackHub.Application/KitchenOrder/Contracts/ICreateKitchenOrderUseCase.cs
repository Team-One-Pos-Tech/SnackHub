using SnackHub.Application.KitchenOrder.Models;

namespace SnackHub.Application.KitchenOrder.Contracts;

public interface ICreateKitchenOrderUseCase
{
    Task<CreateKitchenOrderResponse> Execute(CreateKitchenOrderRequest request);
}