using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class Product
{
    public int Id { get; set; }
    public int CannabisTypeId { get; set; }
    public int AgronomistId { get; set; }
    public int? Price { get; set; }

    public virtual Agronomist Agronomist { get; set; } = null!;

    public virtual CannabisType CannabisType { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductStorage? ProductStorage { get; set; }

    public virtual ICollection<ReturnDetail> ReturnDetails { get; set; } = new List<ReturnDetail>();

    public virtual ICollection<Tasting> Tastings { get; set; } = new List<Tasting>();
}
