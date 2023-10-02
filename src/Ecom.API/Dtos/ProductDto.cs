using Ecom.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ecom.API.Dtos
{
    public class BaseProductDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        

    }
    public class ProductDto:BaseProductDto
    {
        public int Id { get; set; }
        
        public string CategoryName { get; set; }

    }
    public class CreateProductDto:BaseProductDto
    {
        
        
        public int CategoryId { get; set; }

        public IFormFile Image { get; set; }

    }
}
