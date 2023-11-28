namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public int CannabisTypeId { get; set; }

        public int AgronomistId { get; set; }

        public int Price { get; set; }
    }
}
