using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class TastingUpsertDto
    {
        [Required]
        public DateTime? Date { get; set; }
    }
}
