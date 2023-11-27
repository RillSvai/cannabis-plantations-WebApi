using System;
using System.Collections.Generic;

namespace CannabisPlantations.WebApi.Models;

public partial class ApplicationCredential
{
    public string Id { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int ApplicationId { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ICollection<Scope> Scopes { get; set; } = new List<Scope>();
}
