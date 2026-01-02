using Egolance.Application.DTOs.Workers;
using Egolance.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Egolance.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly WorkerService _service;

        public WorkersController(WorkerService service)
        {
            _service = service;
        }

        // CREATE
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkerRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var worker = await _service.CreateWorkerAsync(userId, request);

            return Ok(new WorkerResponse
            {
                WorkerId = worker.WorkerId,
                Bio = worker.Bio,
                HourlyRate = worker.HourlyRate,
                ServiceCategoryId = worker.ServiceCategoryId,
                ExperienceYears = worker.ExperienceYears,
                LocationLat = (double)worker.LocationLat,
                LocationLng = (double)worker.LocationLng,
                IsAvailable = worker.IsAvailable,
                Rating = worker.Rating,
                VerificationStatus = worker.VerificationStatus.ToString()
            });
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var workers = await _service.GetAllAsync();
            return Ok(workers.Select(w => new WorkerResponse
            {
                WorkerId = w.WorkerId,
                Bio = w.Bio,
                HourlyRate = w.HourlyRate,
                ServiceCategoryId = w.ServiceCategoryId,
                ExperienceYears = w.ExperienceYears,
                LocationLat = (double)w.LocationLat,
                LocationLng = (double)w.LocationLng,
                IsAvailable = w.IsAvailable,
                Rating = w.Rating,
                VerificationStatus = w.VerificationStatus.ToString()
            }));
        }

        // GET ONE
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var worker = await _service.GetByIdAsync(id);
            if (worker == null) return NotFound();

            return Ok(new WorkerResponse
            {
                WorkerId = worker.WorkerId,
                Bio = worker.Bio,
                HourlyRate = worker.HourlyRate,
                ServiceCategoryId = worker.ServiceCategoryId,
                ExperienceYears = worker.ExperienceYears,
                LocationLat = (double)worker.LocationLat,
                LocationLng = (double)worker.LocationLng,
                IsAvailable = worker.IsAvailable,
                Rating = worker.Rating,
                VerificationStatus = worker.VerificationStatus.ToString()
            });
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, WorkerUpdate input)
        {
            var worker = await _service.UpdateAsync(id, input);
            if (worker == null) return NotFound();

            return Ok(new WorkerResponse
            {
                WorkerId = worker.WorkerId,
                Bio = worker.Bio,
                HourlyRate = worker.HourlyRate,
                ServiceCategoryId = worker.ServiceCategoryId,
                ExperienceYears = worker.ExperienceYears,
                LocationLat = (double)worker.LocationLat,
                LocationLng = (double)worker.LocationLng,
                IsAvailable = worker.IsAvailable,
                Rating = worker.Rating,
                VerificationStatus = worker.VerificationStatus.ToString()
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }



}
