using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class Application
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ApplicationCredential> ApplicationCredentials { get; set; } = new List<ApplicationCredential>();
}
