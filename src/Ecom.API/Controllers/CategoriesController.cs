using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Getallascyn : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public Getallascyn(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task <ActionResult> GetAllAsync()
        {
            var allcategory= await _uow.CategoryRepository.GetAllAsync();
            if (allcategory is not null)
            {
                return Ok(allcategory);
            }
            return BadRequest("Not Found");
        }
    }
}
