using AutoMapper;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;

namespace CannabisPlantations.WebApi.Utility
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductDto>();

            CreateMap<Customer,CustomerDto>(); 

            CreateMap<Agronomist,AgronomistDto>(); 
            
            CreateMap<Order,OrderDto>();
            CreateMap<OrderDetail,OrderDetailDto>();
            
            CreateMap<Harvest, HarvestDto>();

            CreateMap<Tasting, TastingDto>();

            CreateMap<Feedback, FeedbackDto>();

            CreateMap<BusinessTrip, BusinessTripDto>();

            CreateMap<Return, ReturnDto>();
            CreateMap<ReturnDetail, ReturnDetailDto>();

            CreateMap<ProductStorage, ProductStorageDto>();

            CreateMap<CannabisType, CannabisTypeDto>();
        }
    }
}
