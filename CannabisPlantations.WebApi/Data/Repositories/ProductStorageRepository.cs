using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class ProductStorageRepository : Repository<ProductStorage>, IProductStorageRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductStorageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override async Task InsertAsync(ProductStorage entity)
        {
            ProductStorage? duplicate = await _db.ProductStorage.FirstOrDefaultAsync(ps => ps.ProductId == entity.ProductId);
            if (duplicate is null) 
            {
                await base.InsertAsync(entity);
                return;
            }
            duplicate.Quantity += entity.Quantity;
            _db.ProductStorage.Update(duplicate);
            
        }
    }
}
