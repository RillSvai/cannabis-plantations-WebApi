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
    }
}
