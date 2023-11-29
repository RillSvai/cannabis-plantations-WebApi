using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Customer entity)
        {
            _db.RemoveRange(_db.CustomerTastings.Where(ct => ct.CustomerId == entity.Id));
            _db.RemoveRange(_db.Feedbacks.Where(f => f.CustomerId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Customer> entities)
        {
            IEnumerable<CustomerTastings> customerTastings = _db.CustomerTastings.Where(ct => entities.Any(c => c.Id == ct.CustomerId));
            IEnumerable<Feedback> feedbacks = _db.Feedbacks.Where(f => entities.Any(c => c.Id == f.CustomerId));
            _db.CustomerTastings.RemoveRange(customerTastings);
            _db.Feedbacks.RemoveRange(feedbacks);
            base.DeleteRange(entities);
        }
    }
}
