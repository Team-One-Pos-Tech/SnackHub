using Microsoft.AspNetCore.Mvc;
using SnackHub.Application.Payment.Contracts;
using SnackHub.Application.Payment.Models;

namespace SnackHub.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class WebhookController : ControllerBase
    {
        private readonly IPaymentUseCases _paymentService;

        public WebhookController(IPaymentUseCases paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("payment-status")]
        public async Task<IActionResult> PaymentStatus([FromBody] PaymentStatusDto paymentStatus)
        {
            if (paymentStatus == null)
            {
                return BadRequest();
            }

            var response = await _paymentService.UpdatePaymentStatusAsync(paymentStatus);
            if (!response.IsValid)
            {
                return ValidationProblem(response.Notifications.FirstOrDefault());
            }

            return Ok();
        }
    }
}
