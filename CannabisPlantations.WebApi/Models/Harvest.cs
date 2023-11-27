using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Harvest
{
    public int Id { get; set; }

    public int AgronomistId { get; set; }

    public int CannabisTypeId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? Date { get; set; }

    public virtual Agronomist Agronomist { get; set; } = null!;

    public virtual CannabisType CannabisType { get; set; } = null!;
}
