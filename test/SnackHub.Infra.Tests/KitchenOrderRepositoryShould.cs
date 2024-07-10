using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;
using SnackHub.Infra.Repositories.MongoDB;
using SnackHub.Infra.Tests.Base;

namespace SnackHub.Infra.Tests;

public class KitchenOrderRepositoryShould : IntegrationTestFixture
{
    private KitchenOrderRepository _repository;
    
    [SetUp]
    public void SetUp()
    {
        var logger = new Logger<KitchenOrderRepository>(NullLoggerFactory.Instance);
        _repository = new KitchenOrderRepository(logger, MongoDatabase);
    }
    
    [Test]
    public async Task ListOrdersSortedByCreationDate()
    {
        var randomizedOrdersTasks = Enumerable.Range(1, 5)
                .Select(_ => DelayedCreateOrder())
                .OrderBy(_ => Random.Shared.Next());
        var randomizedOrders = await Task.WhenAll(randomizedOrdersTasks);
        await AddManyOrdersAsync(randomizedOrders);
        
        var orders = await _repository.ListAllAsync();

        orders
            .Should()
            .BeInAscendingOrder(o => o.CreatedAt);
    }
    
    [Test]
    public async Task ListCurrentOrdersSortedByStatusAndCreationDate()
    {
        var order1 = await DelayedCreateOrder(KitchenOrderStatus.Received);
        var order2 = await DelayedCreateOrder(KitchenOrderStatus.Preparing);
        var order3 = await DelayedCreateOrder(KitchenOrderStatus.Preparing);
        var order4 = await DelayedCreateOrder(KitchenOrderStatus.Done);
        var order5 = await DelayedCreateOrder(KitchenOrderStatus.Finished);
        
        var randomizedOrders = new[] { order1, order2, order3, order4, order5 }
            .OrderBy(_ => Random.Shared.Next())
            .ToArray();
        
        await AddManyOrdersAsync(randomizedOrders);
        
        var orders = await _repository.ListCurrentAsync();

        orders
            .Should()
            .ContainInConsecutiveOrder(order4, order2, order3, order1);
    }
    
    private static async Task<KitchenOrder> DelayedCreateOrder(KitchenOrderStatus status = KitchenOrderStatus.Received)
    {
        // Simulate a delay in order creation to ensure that the CreatedAt property is different
        await Task.Delay(50);
        return new KitchenOrder(Guid.NewGuid(), Guid.NewGuid(), [], status);
    }
    
    private async Task AddManyOrdersAsync(params KitchenOrder[] orders)
    {
        foreach(var order in orders)
        {
            await _repository.AddAsync(order);
        }
    }
}