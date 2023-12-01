using CannabisPlantations.WebApi.Models;

namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IAgronomistBusinessTripRepository : IRepository<AgronomistBusinessTrips>
    {
        IEnumerable<BusinessTrip?> GetBusinessTrips(int agronomistId);
        IEnumerable<Agronomist?> GetAgronomists(int businessTripId);
    }
}
