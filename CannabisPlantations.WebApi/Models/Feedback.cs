using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Text { get; set; }

    public DateTime? Date { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
