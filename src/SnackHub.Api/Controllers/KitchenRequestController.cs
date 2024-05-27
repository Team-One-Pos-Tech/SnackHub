using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.KitchenRequest.Contracts;
using SnackHub.Application.KitchenRequest.Models;
using SnackHub.Extensions;

namespace SnackHub.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class KitchenRequestController : ControllerBase
{
    private readonly IListKitchenRequestUseCase _listKitchenRequestUseCase;
    private readonly IUpdateKitchenRequestStatusUseCase _updateKitchenRequestStatusUseCase;

    public KitchenRequestController(IListKitchenRequestUseCase listKitchenRequestUseCase, IUpdateKitchenRequestStatusUseCase updateKitchenRequestStatusUseCase)
    {
        _listKitchenRequestUseCase = listKitchenRequestUseCase;
        _updateKitchenRequestStatusUseCase = updateKitchenRequestStatusUseCase;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<KitchenRequestResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<KitchenRequestResponse>>> GetAll()
    {
        var requests = await _listKitchenRequestUseCase.Execute();
        return Ok(requests);
    }
    
    [HttpPut("UpdateStatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateKitchenRequestStatusResponse>> UpdateStatus([FromBody] UpdateKitchenRequestStatusRequest kitchenRequestStatusRequest)
    {
        var response = await _updateKitchenRequestStatusUseCase.Execute(kitchenRequestStatusRequest);
        return response.IsValid 
            ? Ok(response) 
            : ValidationProblem(ModelState.AddNotifications(response.Notifications));

    }
    
}