using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class AgronomistUpsertDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = null!;

        public bool IsAvailable { get; set; }
    }
}
