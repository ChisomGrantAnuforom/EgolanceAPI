using Egolance.Application.Services;
using Egolance.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Egolance.Api.Controllers
{
    [ApiController]
    [Route("api/stripe/webhook")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly IConfiguration _config;

        public StripeWebhookController(PaymentService paymentService, IConfiguration config)
        {
            _paymentService = paymentService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var secret = _config["Stripe:WebhookSecret"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    secret
                );

                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var intent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdatePaymentStatusAsync(intent.Id, PaymentStatus.Succeeded);
                }

                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    var intent = stripeEvent.Data.Object as PaymentIntent;
                    await _paymentService.UpdatePaymentStatusAsync(intent.Id, PaymentStatus.Failed);
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }

}
