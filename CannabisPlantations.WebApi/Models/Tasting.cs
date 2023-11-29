using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class Tasting
{
    [Key]
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int AgronomistId { get; set; }

    public DateTime Date { get; set; }
    [ForeignKey(nameof(AgronomistId))]
    public Agronomist Agronomist { get; set; } = null!;
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    public ICollection<CustomerTastings> CustomerTastings { get;} = new List<CustomerTastings>();
}
