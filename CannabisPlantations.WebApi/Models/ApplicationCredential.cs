using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models;

public partial class ApplicationCredential
{
    [Key]
    public string Id { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int ApplicationId { get; set; }
    [ForeignKey(nameof(ApplicationId))]
    public Application Application { get; set; } = null!;

    public ICollection<ApplicationCredentialScopes> ApplicationCredentialScopes { get; } = null!;
}
