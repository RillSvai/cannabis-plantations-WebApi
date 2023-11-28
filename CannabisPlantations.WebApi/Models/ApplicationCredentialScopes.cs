using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models
{
    public class ApplicationCredentialScopes
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationCredentialId { get; set; } = null!;
        public int ScopeId { get; set; }
        [ForeignKey(nameof(ApplicationCredentialId))]
        public ApplicationCredential ApplicationCredential { get; set; } = null!;
        [ForeignKey(nameof(ScopeId))]
        public Scope Scope { get; set; } = null!;
    }
}
