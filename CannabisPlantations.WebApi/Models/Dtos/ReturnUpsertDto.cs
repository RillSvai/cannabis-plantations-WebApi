using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ReturnUpsertDto
    {
        [Required]
        public DateTime? Date { get; set; }
    }
}
