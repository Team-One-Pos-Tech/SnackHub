using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.Order.UseCases;

public class ListOrderUseCase : IListOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public ListOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderResponse>> Execute()
    {
        var orders = await _orderRepository.ListAllAsync();
        
        return orders.Select(o => new OrderResponse
        {
            Id = o.Id,
            Items = o.Items.Select(i => (i.ProductName, i.Quantity)).ToList(),
            Status = o.Status.ToString(),
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        }).ToList();
    }
}