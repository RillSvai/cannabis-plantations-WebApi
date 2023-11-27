﻿using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class ProductStorage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
