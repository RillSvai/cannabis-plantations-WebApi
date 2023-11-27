using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        [Range(10, 1000000)]
        public int? Price { get; set; }
    }
}
