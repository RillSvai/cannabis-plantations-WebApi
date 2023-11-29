using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ProductStorageUpsertDto
    {
        [Required]
        [Range(1, 10000000)]
        public int? Quantity { get; set; }
    }
}
