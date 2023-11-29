using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IAgronomistRepository : IRepository<Agronomist>
    {
        public IEnumerable<Customer?> GetCustomersByMinSales(int agronomistId, int salesNumber, DateTime since, DateTime until);
    }
}
