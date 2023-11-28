using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class Product
{
    public int Id { get; set; }
    public int CannabisTypeId { get; set; }
    public int AgronomistId { get; set; }
    public int Price { get; set; }

    [ForeignKey(nameof(AgronomistId))]
    public Agronomist Agronomist { get; set; } = null!;
    [ForeignKey(nameof(CannabisTypeId))]

    public CannabisType CannabisType { get; set; } = null!;

    public ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public ProductStorage? ProductStorage { get; set; }
   
    public ICollection<ReturnDetail> ReturnDetails { get; } = new List<ReturnDetail>();

    public ICollection<Tasting> Tastings { get; set; } = new List<Tasting>();
}
