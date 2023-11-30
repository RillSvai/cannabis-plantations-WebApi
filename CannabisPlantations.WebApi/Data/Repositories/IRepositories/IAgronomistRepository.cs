using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IAgronomistRepository : IRepository<Agronomist>
    {
        public IEnumerable<Customer?> GetCustomersByMinSales(int agronomistId, int salesNumber, DateTime since, DateTime until);
        public IEnumerable<Agronomist?> GetAgronomistCompanions(int agronomistId, DateTime since, DateTime until);
        public int GetNumberTastingsWithDifferenCustomers(int agronomistId, int customerNumber, DateTime since, DateTime until);
    }
}
