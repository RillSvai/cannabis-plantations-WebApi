using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class BusinessTripUpsertDto
    {
        [Required]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
