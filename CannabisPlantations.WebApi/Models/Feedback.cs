using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class Feedback
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Text { get; set; }

    public DateTime Date { get; set; }
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;
}
