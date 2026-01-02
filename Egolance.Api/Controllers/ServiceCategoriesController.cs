using Egolance.Application.DTOs.ServiceCategories;
using Egolance.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace Egolance.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly ServiceCategoryService _service;

        public ServiceCategoriesController(ServiceCategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCategoryInput input)
        {
            var result = await _service.CreateAsync(input);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ServiceCategoryUpdate input)
        {
            var result = await _service.UpdateAsync(id, input);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}


//namespace Egolance.Api.Controllers
//{


//    [ApiController]
//    [Route("api/[controller]")]
//    public class ServiceCategoriesController : ControllerBase
//    {
//        private readonly ServiceCategoryService _service;

//        public ServiceCategoriesController(ServiceCategoryService service)
//        {
//            _service = service;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(ServiceCategoryInput input)
//        {
//            var category = await _service.CreateAsync(input);

//            return Ok(new ServiceCategoryResponse
//            {
//                Id = category.Id,
//                Name = category.Name
//            });
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var categories = await _service.GetAllAsync();

//            return Ok(categories.Select(c => new ServiceCategoryResponse
//            {
//                Id = c.Id,
//                Name = c.Name
//            }));
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> Get(Guid id)
//        {
//            var category = await _service.GetByIdAsync(id);
//            if (category == null) return NotFound();

//            return Ok(new ServiceCategoryResponse
//            {
//                Id = category.Id,
//                Name = category.Name
//            });
//        }
//    }

//}
