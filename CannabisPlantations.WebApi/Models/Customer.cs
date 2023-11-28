using System.ComponentModel.DataAnnotations;

namespace CannabisPlantations.WebApi.Models;

public partial class Customer
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Return> Returns { get; set; } = new List<Return>();

    public ICollection<CustomerTastings> Tastings { get; set; } = new List<CustomerTastings>();
}
