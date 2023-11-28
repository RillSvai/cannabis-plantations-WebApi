namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class AgronomistDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public bool IsAvailable { get; set; }
    }
}
