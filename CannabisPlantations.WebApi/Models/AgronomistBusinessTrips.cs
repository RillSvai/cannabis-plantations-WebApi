using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models
{
    public class AgronomistBusinessTrips
    {
        [Key]
        public int Id { get; set; }
        public int BusinessTripId { get; set; }
        public int AgronomistId { get; set; }
        [ForeignKey(nameof(BusinessTripId))]
        public BusinessTrip BusinessTrip { get; set; } = null!;
        [ForeignKey(nameof(AgronomistId))]
        public Agronomist Agronomist { get; set; } = null!;
    }
}
