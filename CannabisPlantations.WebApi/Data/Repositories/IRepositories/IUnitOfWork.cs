namespace CannabisPlantations.WebApi.Data.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepo { get; }
        ICannabisTypeRepository CannabisTypeRepo { get; }
        IAgronomistRepository AgronomistRepo { get; }
        ICustomerRepository CustomerRepo { get; }
        IOrderRepository OrderRepo { get; }
        IHarvestRepository HarvestRepo { get; }
        ITastingRepository TastingRepo { get; }
        IFeedbackRepository FeedbackRepo { get; }
        IBusinessTripRepository BusinessTripRepo { get; }
        Task Save();
    }
}
