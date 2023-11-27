using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public int? Price { get; set; }
    }
}
