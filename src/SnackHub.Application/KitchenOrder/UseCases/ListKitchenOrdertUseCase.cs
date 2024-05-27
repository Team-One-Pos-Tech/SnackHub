using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.KitchenOrder.UseCases;

public class ListKitchenOrdertUseCase : IListKitchenOrdertUseCase
{
    private readonly IKitchenOrderRepository _kitchenOrderRepository;

    public ListKitchenOrdertUseCase(IKitchenOrderRepository kitchenOrderRepository)
    {
        _kitchenOrderRepository = kitchenOrderRepository;
    }

    public async Task<IEnumerable<KitchenOrderResponse>> Execute()
    {
        var kitchenRequests = await _kitchenOrderRepository.ListAllAsync();

        return (from kitchenRequest in kitchenRequests
            let items = kitchenRequest
                .Items
                .Select(requestItem =>
                    new KitchenOrderResponse.KitchenOrderItem(requestItem.ProductName, requestItem.Quantity))
                .ToList()
            select new KitchenOrderResponse
            {
                OrderId = kitchenRequest.OrderId,
                Items = items,
                Status = kitchenRequest.Status.ToString()
            }).ToList();
    }
}