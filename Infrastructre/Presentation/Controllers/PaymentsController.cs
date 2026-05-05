using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.BasketModuleDTos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePayment(string basketId)
        {
            var result = await _serviceManager.PaymentService.CreateOrUpdatePaymentIntent(basketId);
            return Ok(result);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            await _serviceManager.PaymentService.
                UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]!);

            return new EmptyResult();
        }
    }
}
