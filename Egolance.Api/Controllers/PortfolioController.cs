using Egolance.Application.DTOs.Workers;
using Egolance.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Egolance.Api.Controllers
{

    [ApiController]
    [Route("api/workers/{workerId}/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioService _service;

        public PortfolioController(PortfolioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid workerId, PortfolioItemInput input)
        {
            var item = await _service.AddItemAsync(workerId, input);

            return Ok(new PortfolioItemResponse
            {
                Id = item.Id,
                Title = item.Title,
                FileUrl = item.FileUrl,
                FileType = item.FileType,
                CreatedAt = item.CreatedAt
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid workerId)
        {
            var items = await _service.GetWorkerPortfolioAsync(workerId);

            return Ok(items.Select(i => new PortfolioItemResponse
            {
                Id = i.Id,
                Title = i.Title,
                FileUrl = i.FileUrl,
                FileType = i.FileType,
                CreatedAt = i.CreatedAt
            }));
        }


        [HttpPut("{itemId}")]
        public async Task<IActionResult> Update(Guid workerId, Guid itemId, PortfolioItemUpdate input)
        {
            var item = await _service.UpdateItemAsync(itemId, input);
            if (item == null) return NotFound();

            return Ok(new PortfolioItemResponse
            {
                Id = item.Id,
                Title = item.Title,
                FileUrl = item.FileUrl,
                FileType = item.FileType,
                CreatedAt = item.CreatedAt
            });
        }



        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(Guid workerId, Guid itemId)
        {
            var deleted = await _service.DeleteItemAsync(itemId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }


}
