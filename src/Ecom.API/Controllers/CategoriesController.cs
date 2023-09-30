using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CategoriesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var allcategory = await _uow.CategoryRepository.GetAllAsync();
            if (allcategory is not null)
            {
                return Ok(allcategory);
            }
            return BadRequest("Not Found");
        }

        [HttpGet("Id")]
        public async Task<ActionResult> GetById (int id)
        {
            var cat = await _uow.CategoryRepository.GetAsync(id);
            if (cat is not null) {
            return Ok(cat);
            }
            return BadRequest();
        }
    }
}
