using FluentAssertions;
using Moq;
using SnackHub.Application.Order.Models;
using SnackHub.Application.Order.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

using ClientEntity = SnackHub.Domain.Entities.Client;

namespace SnackHub.Application.Tests.UseCases;

public class ConfirmOrderShould
{
    private ClientEntity _clientMock;
    private Mock<IClientRepository> _clientRepositoryMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IOrderRepository> _orderRepositoryMock;
    private ConfirmOrderUseCase _confirmOrderUseCase;
    
    [SetUp]
    public void Setup()
    {
        var clientId = Guid.NewGuid();
        _clientMock = new ClientEntity(clientId, "client", new CPF("123.456.789-09"));
        
        _clientRepositoryMock = new Mock<IClientRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        
        _confirmOrderUseCase = new ConfirmOrderUseCase(
            _clientRepositoryMock.Object,
            _productRepositoryMock.Object,
            _orderRepositoryMock.Object
        );
    }
    
    [Test]
    public async Task FailWhenClientIsNotFound()
    {
        var request = new ConfirmOrderRequest
        {
            Identifier = Guid.NewGuid().ToString(),
            Items = []
        };
        _clientRepositoryMock
            .Setup(repository => repository.GetClientByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ClientEntity)null!);
        
        var response = await _confirmOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeFalse();
        response
            .Notifications
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(new
            {
                Key = nameof(request.Identifier),
                Message = "Client not found"
            });
    }

    [Test]
    public async Task FailWhenSomeProductIsNotFound()
    {
        var request = new ConfirmOrderRequest
        {
            Identifier = Guid.NewGuid().ToString(),
            Items = [new ConfirmOrderRequest.Item(Guid.NewGuid(), 1)]
        };
        _clientRepositoryMock
            .Setup(repository => repository.GetClientByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_clientMock);
        _productRepositoryMock
            .Setup(repository => repository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync([]);
        
        var response = await _confirmOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeFalse();
        response
            .Notifications
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(new
            {
                Key = nameof(request.Items),
                Message = "One or more products could not be found"
            });
    }

    [Test]
    public async Task FailOnInvalidOrder()
    {
        var request = new ConfirmOrderRequest
        {
            Identifier = _clientMock.Id.ToString(),
            Items = []
        };
        _clientRepositoryMock
            .Setup(repository => repository.GetClientByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_clientMock);
        _productRepositoryMock
            .Setup(repository => repository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync([]);
        
        var response = await _confirmOrderUseCase.Execute(request);
        
        response
            .IsValid
            .Should()
            .BeFalse();
        response
            .Notifications
            .Should()
            .ContainSingle()
            .Which
            .Should()
            .BeEquivalentTo(new
            {
                Key = "Order",
                Message = "Order must have at least one item to be confirmed"
            });
    }

    [Test]
    public async Task SearchClientById()
    {
        var request = new ConfirmOrderRequest
        {
            Identifier = _clientMock.Id.ToString(),
            Items = []
        };
        _clientRepositoryMock
            .Setup(repository => repository.GetClientByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_clientMock);
        
        await _confirmOrderUseCase.Execute(request);
        
        _clientRepositoryMock
            .Verify(repository => repository.GetClientByIdAsync(_clientMock.Id), Times.Once);
    }
    
    [Test]
    public async Task SearchClientByCpf()
    {
        var request = new ConfirmOrderRequest
        {
            Identifier = _clientMock.CPF.Value,
            Items = []
        };
        _clientRepositoryMock
            .Setup(repository => repository.GetClientByCpfAsync(It.IsAny<CPF>()))
            .ReturnsAsync(_clientMock);
        
        await _confirmOrderUseCase.Execute(request);
        
        _clientRepositoryMock
            .Verify(repository => repository.GetClientByCpfAsync(_clientMock.CPF), Times.Once);
    }

    [Test]
    public async Task SucceedWhenRequirementsAreMet()
    {
        var product = new Product("Product", Category.Snack, 10m, "Description", []);
        var request = new ConfirmOrderRequest
        {
            Identifier = _clientMock.Id.ToString(),
            Items = [new ConfirmOrderRequest.Item(product.Id, 1)]
        };
        _productRepositoryMock
            .Setup(repository => repository.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync([product]);
        _clientRepositoryMock
            .Setup(repository => repository.GetClientByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(_clientMock);
        var captures = new List<Domain.Entities.Order>();
        _orderRepositoryMock.Setup(repository => repository.AddAsync(Capture.In(captures)));
        
        var response = await _confirmOrderUseCase.Execute(request);
        
        var order = captures.Single();
        response
            .IsValid
            .Should()
            .BeTrue();
        response
            .Should()
            .BeEquivalentTo(new
            {
                OrderId = order.Id,
                Total = 10m
            });
        order
            .Should()
            .BeEquivalentTo(new
            {
                Id = response.OrderId,
                ClientId = _clientMock.Id,
                Items = new[]
                {
                    new
                    {
                        ProductId = product.Id,
                        ProductName = "Product",
                        UnitPrice = 10m,
                        Quantity = 1
                    }
                }
            });
        _clientRepositoryMock
            .Verify(repository => repository.GetClientByIdAsync(_clientMock.Id), Times.Once);
        _productRepositoryMock
            .Verify(repository => repository.GetByIdsAsync(new[] { product.Id }), Times.Once);
        _orderRepositoryMock
            .Verify(repository => repository.AddAsync(It.IsAny<Domain.Entities.Order>()), Times.Once);
    }
}