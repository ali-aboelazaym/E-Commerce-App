using AutoMapper;
using Ecom.API.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _Mapper;

        public ProductsController(IUnitOfWork uow , IMapper mapper)
        {
            _uow = uow;
            _Mapper = mapper;
        }

        

        [HttpGet("Get-All-Product")]
        public async Task <ActionResult> Get()
        {
            var prod= await _uow.ProductRepository.GetAllAsync(x=>x.Category);
            var result = _Mapper.Map<List<ProductDto>>(prod);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult>Get(int id)
        {
            var src = await _uow.ProductRepository.GetByIdAsync(id, x => x.Category);
            var Results= _Mapper.Map<ProductDto>(src);
            return Ok(Results);
        }

        [HttpPost]
        public async Task<ActionResult>post(CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var src = _Mapper.Map<Product>(productDto);
                    await _uow.ProductRepository.AddAsync(src);
                    return Ok(src);
                }
                else { return BadRequest(productDto); }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
