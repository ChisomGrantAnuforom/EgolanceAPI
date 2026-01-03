using Egolance.Application.DTOs.Workers;
using Egolance.Application.Services;
using Egolance.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Egolance.Api.Controllers
{

    [ApiController]
    [Route("api/bookings/{bookingId}/review")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewService _service;

        public ReviewsController(ReviewService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReview(Guid bookingId, CreateReviewRequest input)
        {
            var reviewerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = await _service.AddReviewAsync(bookingId, reviewerId, input);

            return Ok(new ReviewResponse
            {
                ReviewId = review.ReviewId,
                CustomerId = review.CustomerId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            });
        }
    }

}
