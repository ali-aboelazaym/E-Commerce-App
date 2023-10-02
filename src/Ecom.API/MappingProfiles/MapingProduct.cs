using AutoMapper;
using Ecom.API.Dtos;
using Ecom.Core.Entities;

namespace Ecom.API.MappingProfiles
{
    public class MapingProduct:Profile
    {
        public MapingProduct()
        {
             CreateMap<Product,ProductDto>()
                .ForMember(d=>d.CategoryName , o=>o.MapFrom(s=>s.Category.Name))
                .ReverseMap();
            CreateMap<CreateProductDto,Product>().ReverseMap();
        }
    }
}
