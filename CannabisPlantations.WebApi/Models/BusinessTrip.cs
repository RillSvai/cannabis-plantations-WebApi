using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class BusinessTrip
{
    public int Id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Agronomist> Agronomists { get; set; } = new List<Agronomist>();
}
