using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Product?> GetPurchasedProducts(int customerId, DateTime since, DateTime until);
        IEnumerable<Agronomist?> GetAgronomistsByMinTastings(int customerId, int tastingsNumber, DateTime since, DateTime until);
        Task<IEnumerable<Agronomist?>> GetAgronomistsByAtLeastOneProductTasting(int customerId, DateTime since, DateTime until);
        IEnumerable<Tasting?> GetCommonTastingsBetweenCustomerAgronomist(int customerId, int agronomistId, DateTime since, DateTime until);
    }
}
