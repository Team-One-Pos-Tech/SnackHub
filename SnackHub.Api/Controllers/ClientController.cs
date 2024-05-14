using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Application.UseCases;
using SnackHub.Domain.Contracts;
using SnackHub.Infra.Repositories;

namespace SnackHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController(
            IGetClientUseCase getClientUseCase,
            IRegisterClientUseCase registerClientUseCase) : ControllerBase
    {

        [HttpGet(Name = "Get")]
        public GetClientResponse Get(Guid id)
        {
            var response = getClientUseCase.Execute(id);
            return response;
        }

        [HttpPost(Name = "Post")]
        public ActionResult<RegisterClientResponse> Post(RegisterClientRequest request)
        {
            var response = registerClientUseCase.Execute(request);

            if (!response.IsValid)
                return BadRequest();
            
            return response;
        }
    }
}
