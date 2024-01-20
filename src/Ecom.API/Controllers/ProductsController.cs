using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Data.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection.Metadata.Ecma335;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _Mapper;
        private readonly IWebHostEnvironment _webHost;

        public ProductsController(IUnitOfWork uow , IMapper mapper, IWebHostEnvironment webHost)
        {
            _uow = uow;
            _Mapper = mapper;
            _webHost = webHost;
        }
       
        [HttpGet("Get-All-Product")]
        public async Task <ActionResult> Get()
        {
            var prod= await _uow.ProductRepository.GetAllAsync();
            var result = _Mapper.Map<List<ProductDto>>(prod);
            return Ok(result);
        }

        [HttpGet("Get-One-Product-ById")]
        public async Task<ActionResult>Get(int id)
        {
            var src = await _uow.ProductRepository.GetByIdAsync(id);
            var Results= _Mapper.Map<ProductDto>(src);
            return Ok(Results);
        }

        [HttpPost("add-new-product")]
        public async Task<ActionResult> Post([FromForm] CreateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.AddAsync(productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-exiting-product/{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.UpdateAsync(id, productDto);
                    return res ? Ok(productDto) : BadRequest(res);
                }
                return BadRequest(productDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-exiting-product/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.DeleteAsyncWithPicture(id);
                    return res ? Ok(res) : BadRequest(res);
                }
                return NotFound($" This is {id} Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
