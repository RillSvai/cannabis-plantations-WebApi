using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class OrderUpsertDto
    {
        [Required]
        public DateTime? Date { get; set; }
    }
}
