using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class ReturnRepository : Repository<Return>, IReturnRepository
    {
        private readonly ApplicationDbContext _db;
        public ReturnRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
