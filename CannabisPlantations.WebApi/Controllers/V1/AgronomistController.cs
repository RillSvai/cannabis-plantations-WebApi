using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using CannabisPlantations.WebApi.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AgronomistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AgronomistController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AgronomistDto>> GetAll() 
        {
            IEnumerable<AgronomistDto> agronomistDtos = _mapper
                .Map<IEnumerable<AgronomistDto>>(_unitOfWork.AgronomistRepo.GetAll());
            return Ok(agronomistDtos);
        }
        [HttpGet("{agronomistId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<AgronomistDto> Get([FromRoute] int agronomistId) 
        {
            AgronomistDto agronomistDto = _mapper
                .Map<AgronomistDto>(HttpContext.Items["agronomist"]);
            return Ok(agronomistDto);
        }
        [HttpGet("{agronomistId:int}/harvests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<HarvestDto> GetHarvests([FromRoute] int agronomistId) 
        {
            IEnumerable<HarvestDto> harvestDtos = _mapper
                .Map<IEnumerable<HarvestDto>>(_unitOfWork.HarvestRepo.GetAll(h => h.AgronomistId == agronomistId));
            return Ok(harvestDtos);
        }
        [HttpGet("{agronomistId:int}/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<OrderDto>> GetOrders([FromRoute] int agronomistId) 
        {
            IEnumerable<OrderDto> orderDtos = _mapper
                .Map<IEnumerable<OrderDto>>(_unitOfWork.OrderRepo.GetAll(o => o.AgronomistId == agronomistId));
            return Ok(orderDtos);
        }
        [HttpGet("{agronomistId:int}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<ProductDto>> GetProducts([FromRoute] int agronomistId) 
        {
            IEnumerable<ProductDto> productDtos = _mapper
                .Map<IEnumerable<ProductDto>>(_unitOfWork.ProductRepo.GetAll(p => p.AgronomistId == agronomistId));
            return Ok(productDtos);
        }
        [HttpGet("{agronomistId:int}/returns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<ReturnDto>> GetReturns([FromRoute] int agronomistId) 
        {
            IEnumerable<ReturnDto> returnDtos = _mapper
                .Map<IEnumerable<ReturnDto>>(_unitOfWork.ReturnRepo.GetAll(r => r.AgronomistId == agronomistId));
            return Ok(returnDtos);
        }
        [HttpGet("{agronomistId:int}/tastings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<TastingDto>> GetTastings([FromRoute] int agronomistId) 
        {
            IEnumerable<TastingDto> tastingDtos = _mapper
                .Map<IEnumerable<TastingDto>>(_unitOfWork.TastingRepo.GetAll(t => t.AgronomistId == agronomistId));
            return Ok(tastingDtos);
        }
        [HttpGet("{agronomistId:int}/business-trips")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<BusinessTripDto>> GetBusinessTrips([FromRoute] int agronomistId) 
        {
            IEnumerable<BusinessTripDto> businessTripDtos = _mapper
                .Map<IEnumerable<BusinessTripDto>>(_unitOfWork.AgronomistBusinessTripRepo.GetBusinessTrips(agronomistId));
            return Ok(businessTripDtos);
        }
        /////
        [HttpGet("{agronomistId:int}/min-sales-duration/customers")]
        [SwaggerOperation(Summary = "1 Query (Description below)", Description = StaticDetails.QueryDescription1)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomersByMinSalesDuration([FromRoute] int agronomistId, [FromQuery] int salesNumber,[FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<CustomerDto> customerDtos = _mapper
                .Map<IEnumerable<CustomerDto>>(_unitOfWork.AgronomistRepo.GetCustomersByMinSales(agronomistId, salesNumber, since, until));
            return Ok(customerDtos);
        }
        [HttpGet("{agronomistId:int}/companions-duration/agronomists")]
        [SwaggerOperation(Summary = "4 Query (Description below)", Description = StaticDetails.QueryDescription4)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<AgronomistDto>> GetAgronomistCompanions([FromRoute] int agronomistId, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<AgronomistDto> agronomistDtos = _mapper
                .Map<IEnumerable<AgronomistDto>>(_unitOfWork.AgronomistRepo.GetAgronomistCompanions(agronomistId, since, until));
            return Ok(agronomistDtos);
        }
        [HttpGet("{agronomistId:int}/tasting-different-customers-duration/times")]
        [SwaggerOperation(Summary = "9 Query (Description below)", Description = StaticDetails.QueryDescription9)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<int> GetNumberTastingsWithDifferenCustomers([FromRoute] int agronomistId, [FromQuery] int customerNumber, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            int res = _unitOfWork.AgronomistRepo.GetNumberTastingsWithDifferenCustomers(agronomistId, customerNumber, since, until);
            return Ok(res);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [TypeFilter(typeof(AgronomistUpsertFilterAttribute))]
        /////
        public async Task<ActionResult<AgronomistDto>> Create([FromBody] AgronomistUpsertDto agronomistDto)
        {
            Agronomist agronomist = new Agronomist() 
            {
                Name = agronomistDto.Name,
                IsAvailable = agronomistDto.IsAvailable
            };      
            await _unitOfWork.AgronomistRepo.InsertAsync(agronomist);
            await _unitOfWork.Save();
            await _unitOfWork.AgronomistBusinessTripRepo
                .InsertRangeAsync((agronomistDto.BusinessTripIds ?? Array.Empty<int>())
                .Distinct()
                .Select(i => new AgronomistBusinessTrips { AgronomistId = agronomist.Id, BusinessTripId = i}));
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new {agronomistId = agronomist.Id}, _mapper.Map<AgronomistDto>(agronomist));
        }
        [HttpPut("{agronomistId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistUpsertFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int agronomistId, [FromBody] AgronomistUpsertDto agronomistDto ) 
        {
            Agronomist agronomist = new Agronomist() 
            {
                Id = agronomistId,
                Name = agronomistDto.Name,
                IsAvailable = agronomistDto.IsAvailable
            };
            _unitOfWork.AgronomistRepo.Update(agronomist);
            await _unitOfWork.Save();
            _unitOfWork.AgronomistBusinessTripRepo.DeleteRange(_unitOfWork.AgronomistBusinessTripRepo.GetAll(abt => abt.AgronomistId == agronomistId));
            await _unitOfWork.AgronomistBusinessTripRepo
               .InsertRangeAsync((agronomistDto.BusinessTripIds ?? new int[0])
               .Distinct()
               .Select(i => new AgronomistBusinessTrips { AgronomistId = agronomistId, BusinessTripId = i }));
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{agronomistId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int agronomistId) 
        {
            Agronomist agronomist = (Agronomist)HttpContext.Items["agronomist"]!;
            _unitOfWork.OrderRepo.DeleteRange(_unitOfWork.OrderRepo.GetAll(o => o.AgronomistId == agronomistId));
            _unitOfWork.ReturnRepo.DeleteRange(_unitOfWork.ReturnRepo.GetAll(r => r.AgronomistId == agronomistId));
            _unitOfWork.TastingRepo.DeleteRange(_unitOfWork.TastingRepo.GetAll(t => t.AgronomistId == agronomistId));
            IEnumerable<Product> products = _unitOfWork.ProductRepo.GetAll(p => p.AgronomistId == agronomistId);
            _unitOfWork.TastingRepo.DeleteRange(_unitOfWork.TastingRepo.GetAll(t => products.Any(p => p.Id == t.ProductId)));
            _unitOfWork.ProductRepo.DeleteRange(products);
            _unitOfWork.AgronomistRepo.Delete(agronomist);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
