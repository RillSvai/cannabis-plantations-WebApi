using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class TastingRepository : Repository<Tasting>, ITastingRepository
    {
        private readonly ApplicationDbContext _db;
        public TastingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
