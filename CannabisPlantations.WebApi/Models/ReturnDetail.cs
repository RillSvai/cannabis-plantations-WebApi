using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class ReturnDetail
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }

    public int? ReturnId { get; set; }

    public int Quantity { get; set; }
    [ForeignKey(nameof(ProductId))]

    public virtual Product Product { get; set; } = null!;
    [ForeignKey(nameof(ReturnId))]

    public virtual Return Return { get; set; } = null!;
}
