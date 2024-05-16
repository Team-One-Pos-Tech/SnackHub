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
        public GetClientResponse Get(Guid id)
        {
            var response = _getClientUseCase.Execute(id);
            return response;
        }

        [HttpPost(Name = "Post")]
        public ActionResult<RegisterClientResponse> Post(RegisterClientRequest request)
        {
            var response = _registerClientUseCase.Execute(request);

            if (!response.IsValid)
                return BadRequest();

            return response;
        }
    }
}