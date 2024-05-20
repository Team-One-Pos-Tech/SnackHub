using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Order.Contracts;
using SnackHub.Application.Order.Models;
using SnackHub.Extensions;

namespace SnackHub.Controllers;

[ApiController]
[Route("api/[controller]/v1/[action]")]
public class OrderController : ControllerBase
{
    private readonly IConfirmOrderUseCase _confirmOrderUseCase;
    private readonly ICancelOrderUseCase _cancelOrderUseCase;

    public OrderController(IConfirmOrderUseCase confirmOrderUseCase, ICancelOrderUseCase cancelOrderUseCase)
    {
        _confirmOrderUseCase = confirmOrderUseCase;
        _cancelOrderUseCase = cancelOrderUseCase;
    }
    
    [HttpPost(Name = "Confirm")]
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
    
    [HttpPost(Name = "Cancel")]
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
    
    // TODO: Implement Checkout endpoint
    // [HttpPost(Name = "Checkout")]
    // [ProducesResponseType(typeof(CheckoutOrderResponse), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    // [ProducesDefaultResponseType]
    // public async Task<ActionResult<CheckoutOrderResponse>> Checkout(CheckoutOrderRequest request)
    // {
    //     var response = await _checkoutOrderUseCase.Execute(request);
    //
    //     return response.IsValid
    //         ? Ok(response) 
    //         : ValidationProblem(ModelState.AddNotifications(response.Notifications));
    // }
}