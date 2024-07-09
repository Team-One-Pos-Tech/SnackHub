using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.KitchenOrder.UseCases;

public class ListKitchenOrderUseCase : IListKitchenOrderUseCase
{
    private readonly IKitchenOrderRepository _kitchenOrderRepository;

    public ListKitchenOrderUseCase(IKitchenOrderRepository kitchenOrderRepository)
    {
        _kitchenOrderRepository = kitchenOrderRepository;
    }

    public async Task<IEnumerable<KitchenOrderResponse>> Execute()
    {
        var kitchenRequests = await _kitchenOrderRepository.ListCurrentAsync();

        return kitchenRequests.Select(o => new KitchenOrderResponse
        {
            OrderId = o.OrderId,
            Items = o.Items.Select(i => (i.ProductName, i.Quantity)).ToList(),
            Status = o.Status.ToString(),
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        }).ToList();
    }
}