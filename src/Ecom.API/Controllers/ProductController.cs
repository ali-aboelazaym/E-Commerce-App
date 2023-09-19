using Ecom.Core.Entities;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Getall()
        {
            List<Product> prod =_context.Products.ToList();
            return Ok(prod);
        }
        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            Product prod1 = _context.Products.FirstOrDefault(p => p.Id == id);
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
