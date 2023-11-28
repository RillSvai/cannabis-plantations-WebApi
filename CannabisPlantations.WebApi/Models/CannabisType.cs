using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class CannabisType
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Harvest> Harvests { get; set; } = new List<Harvest>();
}
