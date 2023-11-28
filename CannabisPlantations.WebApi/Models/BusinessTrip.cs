using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class BusinessTrip
{
    [Key]
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ICollection<AgronomistBusinessTrips> AgronomistBusinessTrips { get; } = new List<AgronomistBusinessTrips>();
}
