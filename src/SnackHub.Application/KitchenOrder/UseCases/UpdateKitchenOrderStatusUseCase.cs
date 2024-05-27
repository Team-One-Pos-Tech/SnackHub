using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.KitchenOrder.UseCases;

public class UpdateKitchenOrderStatusUseCase : IUpdateKitchenOrderStatusUseCase
{
    private readonly IKitchenOrderRepository _kitchenOrderRepository;

    public UpdateKitchenOrderStatusUseCase(IKitchenOrderRepository kitchenOrderRepository)
    {
        _kitchenOrderRepository = kitchenOrderRepository;
    }

    public async Task<UpdateKitchenOrderStatusResponse> Execute(UpdateKitchenOrderStatusRequest orderStatusRequest)
    {
        var response = new UpdateKitchenOrderStatusResponse();

        var kitchenOrder = await _kitchenOrderRepository.GetByOderIdAsync(orderStatusRequest.OrderId);
        if (kitchenOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), "Kitchen request for order not found!");
            return response;
        }

        try
        {
            kitchenOrder.UpdateStatus();
            await _kitchenOrderRepository.EditAsync(kitchenOrder);
        }
        catch (Exception exception)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), exception.Message);
        }

        return response;
    }
}