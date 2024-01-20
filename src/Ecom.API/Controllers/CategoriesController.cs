using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var allcategory = await _uow.CategoryRepository.GetAllAsync();           
            if (allcategory is not null)
            {
                //using maping 
                var res=_mapper.Map<IReadOnlyList<Category>,IReadOnlyList<ListingCategoryDto>>(allcategory);
                //using manual dictionary 

                //var res = allcategory.Select(x => new listingCategoryDto
                //{ 
                //    id=x.Id ,
                //    Name = x.Name,
                //    Description = x.Description,
                //}).ToList();
                return Ok(res);
            }
            return BadRequest("Not Found");
        }

        [HttpGet("Id")]
        public async Task<ActionResult> Get(int id)
        {
            var cat = await _uow.CategoryRepository.GetAsync(id);
            if (cat is not null) {
            return Ok( _mapper.Map<Category, ListingCategoryDto> (cat));
            }
            return BadRequest($"Not Found This Id {id}");
        }

        [HttpPost("add-new-category")]
        public async Task<ActionResult> post(CategoryDto categorydto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newcategory = new Category();
                    newcategory.Name = categorydto.Name;
                    newcategory.Description = categorydto.Description;
                    await _uow.CategoryRepository.AddAsync(newcategory);
                    return Ok(categorydto);
                }
                return BadRequest(categorydto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        //[HttpPut("update-existing-cat")]
        //public async Task<ActionResult> put(updateCategoryDto categorydto)
        //{          
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var oldCategory = await _uow.CategoryRepository.GetAsync(categorydto.id);
        //            if (oldCategory is not null)
        //            {                        
        //                oldCategory.Name = categorydto.Name;
        //                oldCategory.Description = categorydto.Description;
        //                await _uow.CategoryRepository.updateAsync(categorydto.id,oldCategory);
        //                return Ok(categorydto);
        //            }                   
        //        }
        //        return BadRequest("this category not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("delete-existing-category")]
        //public async Task<ActionResult>delete(int id)
        //{
        //    try
        //    {
        //        var oldcat = await _uow.CategoryRepository.GetAsync(id);
        //        if (oldcat is not null)
        //        {
        //            await _uow.ProductRepository.DeleteAsync(id);
        //            return Ok($"This Category {oldcat.Name} deleted successfuly");
        //        }
        //        return BadRequest("this category not found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
