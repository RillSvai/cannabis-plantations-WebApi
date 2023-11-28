namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int AgronomistId { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }
    }
}
