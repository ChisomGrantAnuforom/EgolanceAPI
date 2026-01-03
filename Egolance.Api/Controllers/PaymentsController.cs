using Egolance.Application.DTOs.Payments;
using Egolance.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Egolance.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentsController(PaymentService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("create-intent")]
        public async Task<IActionResult> CreateIntent(CreatePaymentRequest input)
        {
            var customerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var (payment, clientSecret) = await _service.CreatePaymentIntentAsync(input.BookingId, customerId);

            return Ok(new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                BookingId = payment.BookingId,
                CustomerId = payment.CustomerId,
                WorkerId = payment.WorkerId,
                Amount = payment.Amount,
                Currency = payment.Currency,
                Status = payment.Status.ToString(),
                ClientSecret = clientSecret
            });
        }
    }

}
