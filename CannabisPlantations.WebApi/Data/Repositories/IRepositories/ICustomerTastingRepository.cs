using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface ICustomerTastingRepository : IRepository<CustomerTastings>
    {
        IEnumerable<Customer?> GetCustomers(int tastingId);
        IEnumerable<Tasting?> GetTastings(int customerId);
    }
}
