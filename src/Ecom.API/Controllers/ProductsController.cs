using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Data.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _Mapper;

        public ProductsController(ApplicationDbContext context, IUnitOfWork uow , IMapper mapper)
        {
            _context = context;
            _uow = uow;
            _Mapper = mapper;
        }
       
        [HttpGet("Get-All-Product")]
        public async Task <ActionResult> Get()
        {
            var prod= await _uow.ProductRepository.GetAllAsync();
            var result = _Mapper.Map<List<ProductDto>>(prod);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult>Get(int id)
        {
            var src = await _uow.ProductRepository.GetByIdAsync(id);
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
                    var res= await _uow.ProductRepository.AddAsync(productDto);
                    return res? Ok(productDto) : BadRequest (res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
