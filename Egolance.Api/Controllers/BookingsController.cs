using Egolance.Application.DTOs.Booking;
using Egolance.Application.Services;
using Egolance.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Egolance.Api.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _service;

        public BookingsController(BookingService service)
        {
            _service = service;
        }

        // CUSTOMER creates booking
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingRequest input)
        {
            var customerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var booking = await _service.CreateBookingAsync(customerId, input);

            return Ok(ToResponse(booking));
        }

        // WORKER accepts
        [Authorize]
        [HttpPatch("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            var workerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var booking = await _service.AcceptBookingAsync(id, workerId);
            if (booking == null) return NotFound();

            return Ok(ToResponse(booking));
        }

        // WORKER rejects
        [Authorize]
        [HttpPatch("{id}/reject")]
        public async Task<IActionResult> Reject(Guid id)
        {
            var workerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var booking = await _service.RejectBookingAsync(id, workerId);
            if (booking == null) return NotFound();

            return Ok(ToResponse(booking));
        }

        // CUSTOMER cancels
        [Authorize]
        [HttpPatch("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var customerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var booking = await _service.CancelBookingAsync(id, customerId);
            if (booking == null) return NotFound();

            return Ok(ToResponse(booking));
        }

        // WORKER completes
        [Authorize]
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var workerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var booking = await _service.CompleteBookingAsync(id, workerId);
            if (booking == null) return NotFound();

            return Ok(ToResponse(booking));
        }

        private BookingResponse ToResponse(Booking b)
        {
            return new BookingResponse
            {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                WorkerId = b.WorkerId,
                ServiceCategoryId = b.ServiceCategoryId,
                Description = b.Description,
                Address = b.Address,
                ScheduledDate = b.ScheduledDate,
                Price = b.Price,
                Status = b.Status.ToString(),
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            };
        }
    }


}
