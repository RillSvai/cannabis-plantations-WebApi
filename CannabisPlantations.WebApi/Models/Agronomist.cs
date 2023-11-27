using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Agronomist
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual ICollection<Tasting> Tastings { get; set; } = new List<Tasting>();

    public virtual ICollection<BusinessTrip> BusinessTrips { get; set; } = new List<BusinessTrip>();
}
