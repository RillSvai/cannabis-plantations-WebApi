using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Order
{
    public int Id { get; set; }

    public int AgronomistId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Agronomist Agronomist { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
