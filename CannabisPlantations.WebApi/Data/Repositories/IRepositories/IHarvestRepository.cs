using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IHarvestRepository : IRepository<Harvest>
    {
        IEnumerable<Agronomist?> GetAgronomistByDifferentHarvestedTypes(int cannabisTypeNumber, DateTime since, DateTime until);
    }
}
