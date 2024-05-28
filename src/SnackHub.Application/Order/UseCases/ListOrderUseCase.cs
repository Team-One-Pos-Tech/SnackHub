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

        return (from order in orders
            let items = order
                .Items
                .Select(item =>
                    new OrderResponse.OrderItem(item.ProductName, item.Quantity))
                .ToList()
            select new OrderResponse
            {
                Id = order.Id,
                Items = items,
                Status = order.Status.ToString()
            }).ToList();
    }
}