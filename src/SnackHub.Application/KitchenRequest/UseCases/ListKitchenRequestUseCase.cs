using SnackHub.Application.KitchenRequest.Contracts;
using SnackHub.Application.KitchenRequest.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.KitchenRequest.UseCases;

public class ListKitchenRequestUseCase : IListKitchenRequestUseCase
{
    private readonly IKitchenRequestRepository _kitchenRequestRepository;

    public ListKitchenRequestUseCase(IKitchenRequestRepository kitchenRequestRepository)
    {
        _kitchenRequestRepository = kitchenRequestRepository;
    }
    
    public async Task<IEnumerable<KitchenRequestResponse>> Execute()
    {
        var kitchenRequests = await _kitchenRequestRepository.ListAllAsync();

        return (from kitchenRequest in kitchenRequests
            let items = kitchenRequest
                .Items
                .Select(requestItem => new KitchenRequestResponse.RequestItem(requestItem.ProductName, requestItem.Quantity))
                .ToList()
            select new KitchenRequestResponse
            {
                OrderId = kitchenRequest.OrderId, 
                Items = items, 
                Status = kitchenRequest.Status.ToString()
            }).ToList();
    }
}