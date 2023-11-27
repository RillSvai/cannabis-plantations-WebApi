using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class CannabisTypeRepository : Repository<CannabisType>, ICannabisTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CannabisTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
