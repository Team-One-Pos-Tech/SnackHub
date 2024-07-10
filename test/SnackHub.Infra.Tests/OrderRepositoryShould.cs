using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SnackHub.Domain.Entities;
using SnackHub.Infra.Repositories.MongoDB;
using SnackHub.Infra.Tests.Base;

namespace SnackHub.Infra.Tests;

public class OrderRepositoryShould : IntegrationTestFixture
{
    private OrderRepository _repository;
    
    [SetUp]
    public void SetUp()
    {
        var logger = new Logger<OrderRepository>(NullLoggerFactory.Instance);
        _repository = new OrderRepository(logger, MongoDatabase);
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
    
    private static async Task<Order> DelayedCreateOrder()
    {
        // Simulate a delay in order creation to ensure that the CreatedAt property is different
        await Task.Delay(50);
        return new Order(Guid.NewGuid(), []);
    }
    
    private async Task AddManyOrdersAsync(params Order[] orders)
    {
        foreach(var order in orders)
        {
            await _repository.AddAsync(order);
        }
    }
}