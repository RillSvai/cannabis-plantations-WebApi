using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Tasting
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int AgronomistId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Agronomist Agronomist { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
