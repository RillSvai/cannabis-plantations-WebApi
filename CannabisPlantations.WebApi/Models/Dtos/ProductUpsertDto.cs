using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ProductUpsertDto
    {
        [Required]
        [Range(10, 1000000)]
        public int? Price { get; set; }
    }
}
