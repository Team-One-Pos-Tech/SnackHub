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
    public class ClientController : ControllerBase
    {
        private readonly IGetClientUseCase getClientUseCase;
        private readonly IClientRepository clientRepository;

        public ClientController()
        {
            clientRepository = new ClientRepository();
            getClientUseCase = new GetClientUseCase(clientRepository);
        }

        [HttpGet(Name = "GetClient")]
        public GetClientResponse Get(Guid id)
        {
            var response = getClientUseCase.Execute(id);
            return response;
        }
    }
}
