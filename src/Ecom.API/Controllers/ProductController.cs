using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _uow;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [HttpGet]
        public async Task<ActionResult> Getall()
        {
            var prod = await _uow.ProductRepository.GetAllAsync();
            return Ok(prod);
        }
        [HttpGet("id")]
        public async Task<ActionResult> Get(int id)
        {
            var prod1 =await _uow.ProductRepository.GetByIdAsync( id, x => x.Category);
            return Ok(prod1);

        }
        [HttpPost]
        public IActionResult addproduct(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(p);
            }
            return Ok();

        }
        [HttpPut]
        public IActionResult updateprod(int id, Product p)
        {
            Product oldprod = _context.Products.Find(id);
            if (oldprod is not null)
            {
                oldprod.Id = id;
                oldprod.Name = p.Name;
                oldprod.Description = p.Description;
                oldprod.Price = p.Price;

            }
            return Ok(oldprod);
        }
        [HttpDelete]
        public IActionResult deleteprod(int id)
        {
            Product prod = _context.Products.Find(id);
            _context.Products.Remove(prod);
            return Ok(prod);
        }
    }
}
