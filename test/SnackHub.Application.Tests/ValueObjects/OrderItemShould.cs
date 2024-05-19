using FluentAssertions;

using OrderItemFactory = SnackHub.Domain.ValueObjects.OrderItem.Factory;

namespace SnackHub.Application.Tests.ValueObjects;

public class OrderItemShould
{
    [Test]
    public void BeValidatedOnCreation()
    {
        var act = () => OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, 3);
        
        act.Should().NotThrow();
    }
    
    [Test]
    public void ShouldFailOnInvalidProductId()
    {
        var act = () => OrderItemFactory.Create(Guid.Empty, "X-Bacon", 10.0m, 3);
        
        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Test]
    public void ShouldFailOnInvalidProductName()
    {
        var act = () => OrderItemFactory.Create(Guid.NewGuid(), "", 10.0m, 3);
        
        act.Should().ThrowExactly<ArgumentException>();
    }
    
    [Test]
    public void ShouldFailOnInvalidUnitPrice()
    {
        var act = () => OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", -10.0m, 3);
        
        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Test]
    public void ShouldFailOnInvalidQuantity()
    {
        var act = () => OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, -3);
        
        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Test]
    public void ShouldCalculateTotal()
    {
        var orderItem = OrderItemFactory.Create(Guid.NewGuid(), "X-Bacon", 10.0m, 3);
        
        orderItem.Total.Should().Be(30.0m);
    }
}