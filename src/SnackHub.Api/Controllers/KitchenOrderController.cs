using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.KitchenOrder.Contracts;
using SnackHub.Application.KitchenOrder.Models;
using SnackHub.Extensions;

namespace SnackHub.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class KitchenOrderController : ControllerBase
{
    private readonly IListKitchenOrdertUseCase _listKitchenOrdertUseCase;
    private readonly IUpdateKitchenOrderStatusUseCase _updateKitchenOrderStatusUseCase;

    public KitchenOrderController(IListKitchenOrdertUseCase listKitchenOrdertUseCase, IUpdateKitchenOrderStatusUseCase updateKitchenOrderStatusUseCase)
    {
        _listKitchenOrdertUseCase = listKitchenOrdertUseCase;
        _updateKitchenOrderStatusUseCase = updateKitchenOrderStatusUseCase;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<KitchenOrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<KitchenOrderResponse>>> GetAll()
    {
        var requests = await _listKitchenOrdertUseCase.Execute();
        return Ok(requests);
    }
    
    [HttpPut("UpdateStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateKitchenOrderStatusResponse>> UpdateStatus([FromBody] UpdateKitchenOrderStatusRequest kitchenOrderStatusRequest)
    {
        var response = await _updateKitchenOrderStatusUseCase.Execute(kitchenOrderStatusRequest);
        return response.IsValid 
            ? Ok(response) 
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));

    }
    
}