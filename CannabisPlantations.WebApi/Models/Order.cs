using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class Order
{
    [Key]
    public int Id { get; set; }

    public int AgronomistId { get; set; }

    public int CustomerId { get; set; }

    public DateTime Date { get; set; }
    [ForeignKey(nameof(AgronomistId))]
    public Agronomist Agronomist { get; set; } = null!;
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
