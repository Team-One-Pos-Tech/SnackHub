using SnackHub.Application.KitchenRequest.Contracts;
using SnackHub.Application.KitchenRequest.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.KitchenRequest.UseCases;

public class UpdateKitchenRequestStatusUseCase : IUpdateKitchenRequestStatusUseCase
{
    private readonly IKitchenRequestRepository _kitchenRequestRepository;

    public UpdateKitchenRequestStatusUseCase(IKitchenRequestRepository kitchenRequestRepository)
    {
        _kitchenRequestRepository = kitchenRequestRepository;
    }

    public async Task<UpdateKitchenRequestStatusResponse> Execute(UpdateKitchenRequestStatusRequest request)
    {
        var response = new UpdateKitchenRequestStatusResponse();
        
        var kitchenRequest = await _kitchenRequestRepository.GetByOderIdAsync(request.OrderId);
        if (kitchenRequest is null)
        {
            response.AddNotification(nameof(request.OrderId), "Kitchen request for order not found!");
            return response;
        }
        
        try
        {
            kitchenRequest.UpdateStatus();
            await _kitchenRequestRepository.EditAsync(kitchenRequest);
        }
        catch (Exception exception)
        {
            response.AddNotification(nameof(request.OrderId), exception.Message);
        }

        return response;
    }
}