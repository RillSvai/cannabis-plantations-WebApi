using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class AgronomistRepository : Repository<Agronomist>, IAgronomistRepository
    {
        private readonly ApplicationDbContext _db;
        public AgronomistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
