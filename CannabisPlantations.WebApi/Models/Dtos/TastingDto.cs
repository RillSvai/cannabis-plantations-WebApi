namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class TastingDto
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int AgronomistId { get; set; }

        public DateTime Date { get; set; }
    }
}
