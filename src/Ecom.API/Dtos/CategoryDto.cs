using System.ComponentModel.DataAnnotations;

namespace Ecom.API.Dtos
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class updateCategoryDto:CategoryDto
    {
        public int id { get; set; }
    }

    public class listingCategoryDto : CategoryDto
    {
        public int id { get; set; }
    }
}
