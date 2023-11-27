using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    public virtual ICollection<Tasting> Tastings { get; set; } = new List<Tasting>();
}
