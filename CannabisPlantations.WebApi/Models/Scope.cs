using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class Scope
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<ApplicationCredentialScopes> ApplicationCredentialScopes { get; set; } = null!;
}
