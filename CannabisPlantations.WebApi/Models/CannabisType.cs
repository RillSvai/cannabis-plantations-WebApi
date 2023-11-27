using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class CannabisType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();

    public virtual Product? Product { get; set; }
}
