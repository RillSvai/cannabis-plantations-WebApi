namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string? Text { get; set; }

        public DateTime Date { get; set; }
    }
}
