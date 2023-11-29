using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Product?> GetPurchasedProducts(int customerId, DateTime since, DateTime until);
    }
}
