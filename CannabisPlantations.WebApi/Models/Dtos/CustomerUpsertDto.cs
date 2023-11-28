using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class CustomerUpsertDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = null!;
    }
}
