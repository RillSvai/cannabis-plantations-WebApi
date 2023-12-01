namespace CannabisPlantations.WebApi.Models.Dtos
{
    public class ReturnDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int? ReturnId { get; set; }

        public int Quantity { get; set; }
    }
}
