using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Contracts;
using SnackHub.Application.Models;

namespace SnackHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController: ControllerBase
    {
        private readonly IGetClientUseCase _getClientUseCase;
        private readonly IRegisterClientUseCase _registerClientUseCase;

        public ClientController(IGetClientUseCase getClientUseCase, IRegisterClientUseCase registerClientUseCase)
        {
            _getClientUseCase = getClientUseCase;
            _registerClientUseCase = registerClientUseCase;
        }
        
        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var clientResponse = await _getClientUseCase.Execute(id);
            return Ok(clientResponse);
        }

        [HttpPost(Name = "Post")]
        public async Task<IActionResult> Post(RegisterClientRequest request)
        {
            var response = await _registerClientUseCase.Execute(request);

            if (!response.IsValid)
                return BadRequest();

            return Ok(response);
        }
    }
}