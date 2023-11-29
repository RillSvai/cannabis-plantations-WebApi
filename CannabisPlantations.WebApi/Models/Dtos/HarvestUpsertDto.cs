using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class HarvestUpsertDto
    {
        [Required]
        public int? Quantity { get; set; }
        [Required]
        public DateTime? Date { get; set; }
    }
}
