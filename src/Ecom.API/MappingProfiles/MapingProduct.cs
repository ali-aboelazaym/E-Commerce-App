using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;

namespace Ecom.API.MappingProfiles
{
    public class MapingProduct:Profile
    {
        public MapingProduct()
        {
            
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ProductPicture, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            CreateMap<CreateProductDto,Product>().ReverseMap();
            //CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
