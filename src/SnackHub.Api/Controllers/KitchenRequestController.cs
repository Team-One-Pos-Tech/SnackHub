using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.KitchenRequest.Contracts;
using SnackHub.Application.KitchenRequest.Models;

namespace SnackHub.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class KitchenRequestController : ControllerBase
{
    private readonly IListKitchenRequestUseCase _listKitchenRequestUseCase;

    public KitchenRequestController(IListKitchenRequestUseCase listKitchenRequestUseCase)
    {
        _listKitchenRequestUseCase = listKitchenRequestUseCase;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListKitchenRequestResponse>>> GetAll()
    {
        var requests = await _listKitchenRequestUseCase.Execute();
        if(requests.Any())
            return Ok(requests);

        return NotFound();
    }
}