using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class Harvest
{
    [Key]
    public int Id { get; set; }

    public int AgronomistId { get; set; }

    public int CannabisTypeId { get; set; }

    public int Quantity { get; set; }

    public DateTime Date { get; set; }
    [ForeignKey(nameof(AgronomistId))]
    public Agronomist Agronomist { get; set; } = null!;
    [ForeignKey(nameof(CannabisTypeId))]
    public CannabisType CannabisType { get; set; } = null!;
}
