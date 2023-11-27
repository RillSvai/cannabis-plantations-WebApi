﻿using CannabisPlantations.WebApi.Data.Repositories.IRepositories;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ProductRepo = new ProductRepository(db);
            CannabisTypeRepo = new CannabisTypeRepository(db);
            AgronomistRepo = new AgronomistRepository(db);
        }
        public IProductRepository ProductRepo { get; }

        public ICannabisTypeRepository CannabisTypeRepo { get; }

        public IAgronomistRepository AgronomistRepo {get; }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
