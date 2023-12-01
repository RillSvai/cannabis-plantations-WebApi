using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CannabisPlantations.WebApi.Data.Repositories
{
    public class AgronomistRepository : Repository<Agronomist>, IAgronomistRepository
    {
        private readonly ApplicationDbContext _db;
        public AgronomistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Delete(Agronomist entity)
        {
            _db.AgronomistBusinessTrips.RemoveRange(_db.AgronomistBusinessTrips.Where(abt => abt.AgronomistId == entity.Id));
            _db.Harvests.RemoveRange(_db.Harvests.Where(h => h.AgronomistId == entity.Id));
            base.Delete(entity);
        }
        public override void DeleteRange(IEnumerable<Agronomist> entities)
        {
            IEnumerable<AgronomistBusinessTrips> agronomistBusinessTrips = _db.AgronomistBusinessTrips.Where(abt => entities.Any(a => abt.AgronomistId == a.Id));
            IEnumerable<Harvest> harvests = _db.Harvests.Where(h => entities.Any(a => h.AgronomistId == a.Id));
            _db.AgronomistBusinessTrips.RemoveRange(agronomistBusinessTrips);
            _db.Harvests.RemoveRange(harvests);
            base.DeleteRange(entities);
        }

        public IEnumerable<Agronomist?> GetAgronomistCompanions(int agronomistId, DateTime since, DateTime until)
        {
            List<AgronomistBusinessTrips> agronomistBusinessTrips = _db.AgronomistBusinessTrips
                .Join(_db.BusinessTrips, abt => abt.BusinessTripId, bt => bt.Id, (abt, bt) => new
                {
                    AgronomistBusinessTrip = abt,
                    StartDate = bt.StartDate,
                    EndDate = bt.EndDate,
                })
                .Where(abt_bt => abt_bt.StartDate >= since && abt_bt.EndDate <= until)
                .Select(abt_bt => abt_bt.AgronomistBusinessTrip).ToList();
             List<List<int>> agronomistIds = agronomistBusinessTrips
                .GroupBy(abt => abt.BusinessTripId)
                .Where(g => g.Count() >= 2 && g.Any(abt => abt.AgronomistId == agronomistId))
                .Select(g => g.Select(abt => abt.AgronomistId).ToList()).ToList();
             return agronomistIds.SelectMany(list => list)
                .Distinct()
                .Where(i => i != agronomistId)
                .Select(i => _db.Agronomists.FirstOrDefault(a => a.Id == i));
        }

        public IEnumerable<Customer?> GetCustomersByMinSales(int agronomistId, int salesNumber, DateTime since, DateTime until)
        {
            return _db.Orders
                .Where(o => o.AgronomistId == agronomistId && o.Date <= until && o.Date >= since)
                .GroupBy(o => o.CustomerId)
                .Where(g => g.Count() >= salesNumber)
                .Select( g => _db.Customers.FirstOrDefault(c => c.Id == g.Key));
        }

        public int GetNumberTastingsWithDifferenCustomers(int agronomistId, int customerNumber, DateTime since, DateTime until)
        {
            var tastingGroups = _db.CustomerTastings
                .GroupBy(ct => ct.TastingId)
                .ToList();
            return tastingGroups
                .Where(g => g.Count() >= customerNumber)
                .Join(_db.Tastings.Where(t => t.AgronomistId == agronomistId && t.Date >= since && t.Date <= until), g => g.Key, t => t.Id, (g,t) => 1)
                .Sum();
        }
    }
}
