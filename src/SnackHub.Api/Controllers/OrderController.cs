using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Application.Order.UseCases;
using SnackHub.Extensions;

namespace SnackHub.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class OrderController : ControllerBase
{
    private readonly IConfirmOrderUseCase _confirmOrderUseCase;
    private readonly ICancelOrderUseCase _cancelOrderUseCase;
    private readonly ICheckoutOrderUseCase _checkoutOrderUseCase;
    private readonly IListOrderUseCase _listOrderUseCase;
    private readonly ICheckPaymentStatusUseCase _checkPaymentStatusUseCase;
    private readonly IUpdateOrderStatusUseCase _updateOrderStatusUseCase;

    public OrderController(
        IConfirmOrderUseCase confirmOrderUseCase,
        ICancelOrderUseCase cancelOrderUseCase,
        ICheckoutOrderUseCase checkoutOrderUseCase, 
        IListOrderUseCase listOrderUseCase,
        ICheckPaymentStatusUseCase checkPaymentStatusUseCase,
        IUpdateOrderStatusUseCase updateOrderStatusUseCase)
    {
        _confirmOrderUseCase = confirmOrderUseCase;
        _cancelOrderUseCase = cancelOrderUseCase;
        _checkoutOrderUseCase = checkoutOrderUseCase;
        _listOrderUseCase = listOrderUseCase;
        _checkPaymentStatusUseCase = checkPaymentStatusUseCase;
        _updateOrderStatusUseCase = updateOrderStatusUseCase;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll()
    {
        var orders = await _listOrderUseCase.Execute();
        return Ok(orders);
    }
    
    [HttpPost("Confirm")]
    [ProducesResponseType(typeof(ConfirmOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ConfirmOrderResponse>> Confirm(ConfirmOrderRequest request)
    {
        var response = await _confirmOrderUseCase.Execute(request);

        return response.IsValid
            ? Ok(response) 
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    }
    
    [HttpPut("Cancel")]
    [ProducesResponseType(typeof(CancelOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CancelOrderResponse>> Cancel(CancelOrderRequest request)
    {
        var response = await _cancelOrderUseCase.Execute(request);

        return response.IsValid
            ? Ok(response) 
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    }
    
    [HttpPost("Checkout")]
    [ProducesResponseType(typeof(CheckoutOrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CheckoutOrderResponse>> Checkout(CheckoutOrderRequest request)
    {
        var response = await _checkoutOrderUseCase.Execute(request);
    
        return response.IsValid
            ? Ok(response) 
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    }

    [HttpGet("{orderId:guid}/payment-status")]
    [ProducesResponseType(typeof(CheckPaymentStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckPaymentStatusResponse>> CheckPaymentStatus(Guid orderId)
    {
        var request = new CheckPaymentStatusRequest { OrderId = orderId };
        var response = await _checkPaymentStatusUseCase.Execute(request);

        return response.IsValid
            ? Ok(response)
            : NotFound();
    }

    [HttpPut("{orderId:guid}/status")]
    [ProducesResponseType(typeof(UpdateOrderStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UpdateOrderStatusResponse>> UpdateOrderStatus(Guid orderId)
    {
        var request = new UpdateOrderStatusRequest { OrderId = orderId };
        var response = await _updateOrderStatusUseCase.Execute(request);

        return response.IsValid
            ? Ok(response)
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    }


}