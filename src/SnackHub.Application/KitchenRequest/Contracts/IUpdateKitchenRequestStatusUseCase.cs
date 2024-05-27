using SnackHub.Application.KitchenRequest.Models;

namespace SnackHub.Application.KitchenRequest.Contracts;

public interface IUpdateKitchenRequestStatusUseCase
{
    Task<UpdateKitchenRequestStatusResponse> Execute(UpdateKitchenRequestStatusRequest request);
}