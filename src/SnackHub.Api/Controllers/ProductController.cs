using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Contracts;
using SnackHub.Application.Models;
using SnackHub.Extensions;

namespace SnackHub.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductUseCase _getProductUseCase;
        private readonly IManageProductUseCase _manageProductUseCase;

        public ProductController(IGetProductUseCase getProductUseCase, IManageProductUseCase manageProductUseCase)
        {
            _getProductUseCase = getProductUseCase;
            _manageProductUseCase = manageProductUseCase;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetProductResponse>> GetById([FromRoute] Guid id)
        {
            var productResponse = await _getProductUseCase.Execute(id);
            if (productResponse is null)
                return NotFound();

            return Ok(productResponse);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAll()
        {
            var products = await _getProductUseCase.Execute();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ManageProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ManageProductResponse>> Post(ManageProductRequest request)
        {
            var response = await _manageProductUseCase.AddAsync(request);

            if (!response.IsValid)
            {
                return ValidationProblem(ModelState.AddNotifications((IEnumerable<Flunt.Notifications.Notification>)response.Notifications));
            }

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ManageProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ManageProductResponse>> Put(Guid id, ManageProductRequest request)
        {
            var response = await _manageProductUseCase.UpdateAsync(id, request);

            if (!response.IsValid)
            {
                return ValidationProblem(ModelState.AddNotifications((IEnumerable<Flunt.Notifications.Notification>)response.Notifications));
            }

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _manageProductUseCase.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
