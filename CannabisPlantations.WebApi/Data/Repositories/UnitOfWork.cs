using CannabisPlantations.WebApi.Data.Repositories.IRepositories;

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
            CustomerRepo = new CustomerRepository(db);
            OrderRepo = new OrderRepository(db);
            HarvestRepo = new HarvestRepository(db);
            TastingRepo = new TastingRepository(db);
            FeedbackRepo = new FeedbackRepository(db);
            BusinessTripRepo = new BusinessTripRepository(db);
            ReturnRepo = new ReturnRepository(db);
            ProductStorageRepo = new ProductStorageRepository(db);  
            OrderDetailRepo = new OrderDetailRepository(db);
            ReturnDetailRepo = new ReturnDetailRepository(db);
            CustomerTastingRepo = new CustomerTastingRepository(db);
            AgronomistBusinessTripRepo = new AgronomistBusinessTripRepository(db);
        }
        public IProductRepository ProductRepo { get; }
        public ICannabisTypeRepository CannabisTypeRepo { get; }
        public IAgronomistRepository AgronomistRepo {get; }
        public ICustomerRepository CustomerRepo { get; }
        public IOrderRepository OrderRepo { get; }
        public IHarvestRepository HarvestRepo { get; }
        public ITastingRepository TastingRepo { get; }
        public IFeedbackRepository FeedbackRepo { get; }
        public IBusinessTripRepository BusinessTripRepo { get; }
        public IReturnRepository ReturnRepo { get; }
        public IProductStorageRepository ProductStorageRepo { get; }
        public IOrderDetailRepository OrderDetailRepo { get; }
        public IReturnDetailRepository ReturnDetailRepo { get; }
        public ICustomerTastingRepository CustomerTastingRepo { get; }
        public IAgronomistBusinessTripRepository AgronomistBusinessTripRepo {  get; }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
