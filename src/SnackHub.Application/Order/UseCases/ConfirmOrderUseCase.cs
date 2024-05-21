using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Domain.Base;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;

using OrderFactory = SnackHub.Domain.Entities.Order.Factory;
using OrderItemFactory = SnackHub.Domain.ValueObjects.OrderItem.Factory;

namespace SnackHub.Application.Order.UseCases;

public class ConfirmOrderUseCase : IConfirmOrderUseCase 
{
    private readonly IClientRepository _clientRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    
    public ConfirmOrderUseCase(
        IClientRepository clientRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository)
    {
        _clientRepository = clientRepository;
         _productRepository = productRepository;
        _orderRepository = orderRepository;
    }
    
    public async Task<ConfirmOrderResponse> Execute(ConfirmOrderRequest request)
    {
        var response = new ConfirmOrderResponse();
        
        var clientExists = await _clientRepository.ExistsByIdAsync(request.ClientId);
        if (!clientExists)
        {
            response.AddNotification(nameof(request.ClientId), "Client not found");
            return response;
        }
        
        var (orderItems, orderItemsValid) = await GetOrderItems(request.Items);
        if (!orderItemsValid)
        {
            response.AddNotification(nameof(request.Items), "One or more products could not be found");
            return response;
        }

        try
        {
            var order = OrderFactory.Create(request.ClientId, orderItems);
            order.Confirm();
            
            await _orderRepository.AddAsync(order);
            
            response.Id = order.Id;
        } 
        catch (DomainException e)
        {
            response.AddNotification("Order", e.Message);
        }

        return response;
    }
    
    private async Task<(IReadOnlyCollection<OrderItem>, bool)> GetOrderItems(IEnumerable<ConfirmOrderRequest.Item> items)
    {
        var requestItems = items
            .GroupBy(item => item.ProductId)
            .ToDictionary(item => item.Key, group => group.Sum(g => g.Quantity));
        
        var products = await _productRepository.GetByIdsAsync(requestItems.Keys);
        var productMap = products.ToDictionary(product => product.Id);
        if (productMap.Count != requestItems.Count)
        {
            return ([], false);
        }
        
        var orderItems = requestItems
            .Select(item =>
            {
                var product = productMap[item.Key];
                return OrderItemFactory.Create(product.Id, product.Name, product.Price, item.Value);
            })
            .ToList()
            .AsReadOnly();

        return (orderItems, true);
    }
}
