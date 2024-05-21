using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Domain.Base;
using SnackHub.Domain.Contracts;

namespace SnackHub.Application.Order.UseCases;

public class CancelOrderUseCase : ICancelOrderUseCase 
{
    private readonly IOrderRepository _orderRepository;
    
    public CancelOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<CancelOrderResponse> Execute(CancelOrderRequest request)
    {
        var response = new CancelOrderResponse();
        
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order is null)
        {
            response.AddNotification(nameof(request.OrderId), "Order not found");
            return response;
        }

        try
        {
            order.Cancel();
            
            await _orderRepository.EditAsync(order);
        }
        catch (DomainException e)
        {
            response.AddNotification(nameof(request.OrderId), e.Message);
        }
        
        return response;
    }
}
