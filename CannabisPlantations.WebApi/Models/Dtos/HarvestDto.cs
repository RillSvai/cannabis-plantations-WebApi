namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class HarvestDto
    {
        public int Id { get; set; }

        public int AgronomistId { get; set; }

        public int CannabisTypeId { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }
    }
}
