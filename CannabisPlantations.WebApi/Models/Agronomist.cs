using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class Agronomist
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public ICollection<Return> Returns { get; set; } = new List<Return>();

    public ICollection<Tasting> Tastings { get; set; } = new List<Tasting>();
    public ICollection<AgronomistBusinessTrips> AgronomistBusinessTrips { get;} = new List<AgronomistBusinessTrips>();

}
