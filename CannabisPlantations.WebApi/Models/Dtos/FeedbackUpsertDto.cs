using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class FeedbackUpsertDto
    {
        public string? Text { get; set; }

        [Required]
        public DateTime? Date { get; set; }
    }
}
