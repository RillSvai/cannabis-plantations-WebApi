using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class HarvestRepository : Repository<Harvest>, IHarvestRepository
    {
        private readonly ApplicationDbContext _db;
        public HarvestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Agronomist?> GetAgronomistByDifferentHarvestedTypes(int cannabisTypeNumber, DateTime since, DateTime until)
        {
            return _db.Harvests
                .Where(h => h.Date >= since && h.Date <= until)
                .Select(h => new
                {
                    AgronomistId = h.AgronomistId,
                    CannabisTypeId = h.CannabisTypeId
                })
                .GroupBy(ac => ac.AgronomistId)
                .Where(g => g.Count() >= cannabisTypeNumber)
                .Select(g => _db.Agronomists.FirstOrDefault(a => a.Id == g.Key));
        }
    }
}
