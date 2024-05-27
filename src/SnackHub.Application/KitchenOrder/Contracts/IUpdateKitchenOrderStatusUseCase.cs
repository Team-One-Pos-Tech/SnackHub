using SnackHub.Application.KitchenOrder.Models;

namespace SnackHub.Application.KitchenOrder.Contracts;

public interface IUpdateKitchenOrderStatusUseCase
{
    Task<UpdateKitchenOrderStatusResponse> Execute(UpdateKitchenOrderStatusRequest orderStatusRequest);
}