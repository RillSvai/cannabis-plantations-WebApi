using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class ReturnDetail
{
    public int ProductId { get; set; }

    public int ReturnId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Return Return { get; set; } = null!;
}
