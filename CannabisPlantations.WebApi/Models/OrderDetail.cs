using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class OrderDetail
{
    [Key]
    public int Id { get; set; }
    public int? ProductId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;
    [ForeignKey(nameof(ProductId))]

    public Product Product { get; set; } = null!;
}
