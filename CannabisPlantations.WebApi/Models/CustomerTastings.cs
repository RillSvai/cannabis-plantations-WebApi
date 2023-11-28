using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CannabisPlantations.WebApi.Models
{
    public class CustomerTastings
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TastingId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
        [ForeignKey(nameof(TastingId))]
        public Tasting Tasting { get; set; } = null!;

    }
}
