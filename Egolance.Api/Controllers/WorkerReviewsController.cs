using Egolance.Application.DTOs.Workers;
using Egolance.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Egolance.Api.Controllers
{
    [ApiController]
    [Route("api/workers/{workerId}/reviews")]
    public class WorkerReviewsController : ControllerBase
    {
        private readonly ReviewService _service;

        public WorkerReviewsController(ReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(Guid workerId)
        {
            var reviews = await _service.GetWorkerReviewsAsync(workerId);

            return Ok(reviews.Select(r => new ReviewResponse
            {
                ReviewId = r.ReviewId,
                CustomerId = r.CustomerId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }));
        }
    }

}
