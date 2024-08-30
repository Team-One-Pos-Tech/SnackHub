using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Client.Contracts;
using SnackHub.Application.Client.Models;
using SnackHub.Extensions;

namespace SnackHub.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1"), Authorize]
    public class ClientController: ControllerBase
    {
        private readonly IGetClientUseCase _getClientUseCase;
        private readonly IRegisterClientUseCase _registerClientUseCase;

        public ClientController(IGetClientUseCase getClientUseCase, IRegisterClientUseCase registerClientUseCase)
        {
            _getClientUseCase = getClientUseCase;
            _registerClientUseCase = registerClientUseCase;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetClientResponse>> GetById([FromRoute] Guid id)
        {
            var clientResponse = await _getClientUseCase.Execute(id);
            if (clientResponse is null)
                return NotFound();
            
            return Ok(clientResponse);
        }
        
        [HttpGet("{cpf:minlength(11):maxlength(11)}")]
        public async Task<ActionResult<GetClientResponse>> GetByCpf([FromRoute] string cpf)
        {
            var clientResponse = await _getClientUseCase.Execute(cpf);
            if (clientResponse is null)
                return NotFound();
            
            return Ok(clientResponse);
        }

    }
}