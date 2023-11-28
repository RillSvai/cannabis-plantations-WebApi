using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class Application
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<ApplicationCredential> ApplicationCredentials { get; set; } = new List<ApplicationCredential>();
}
