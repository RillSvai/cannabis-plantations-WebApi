using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using CannabisPlantations.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CustomerDto>> GetAll() 
        {
            IEnumerable<CustomerDto> customers = _mapper.Map<IEnumerable<CustomerDto>>(_unitOfWork.CustomerRepo.GetAll());
            return Ok(customers);
        }
        [HttpGet("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public ActionResult<CustomerDto> Get([FromRoute] int customerId) 
        {
            CustomerDto customerDto = _mapper.Map<CustomerDto>(HttpContext.Items["customer"]);
            return Ok(customerDto);
        }
        [HttpGet("{customerId:int}/purchased-duration/products")]
        [SwaggerOperation(Summary = "2 Query (Description below)", Description = StaticDetails.QueryDescription2)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public ActionResult<IEnumerable<ProductDto>> GetPurchasedProducts([FromRoute] int customerId, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<ProductDto> productDtos = _mapper
                .Map<IEnumerable<ProductDto>>
                (_unitOfWork.CustomerRepo.GetPurchasedProducts(customerId, since, until));
            return Ok(productDtos);
        }
        [HttpGet("{customerId:int}/min-tastings-duration/agronomists")]
        [SwaggerOperation(Summary = "3 Query (Description below)", Description = StaticDetails.QueryDescription3)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public ActionResult<IEnumerable<AgronomistDto>> GetAgronomistsByMinTastings([FromRoute] int customerId, [FromQuery] int tastingsNumber, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<AgronomistDto> agronomistDtos = _mapper
                .Map<IEnumerable<AgronomistDto>>
                (_unitOfWork.CustomerRepo.GetAgronomistsByMinTastings(customerId,tastingsNumber,since,until));
            return Ok(agronomistDtos);
        }
        [HttpGet("{customerId:int}/at-least-one-tasting-order-duration/agronomists")]
        [SwaggerOperation(Summary = "5 Query (Description below)", Description = StaticDetails.QueryDescription5)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public async Task<ActionResult<IEnumerable<AgronomistDto>>> GetAgronomistsByAtLeastOneTastingOrder([FromRoute] int customerId, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<AgronomistDto> agronomistDtos = _mapper
                .Map<IEnumerable<AgronomistDto>>
                (await _unitOfWork.CustomerRepo.GetAgronomistsByAtLeastOneProductTasting(customerId, since, until));
            return Ok(agronomistDtos);
        }
        [HttpGet("{customerId:int}/agronomist/{agronomistId:int}/common-duration/tastings")]
        [SwaggerOperation(Summary = "8 Query (Description below)", Description = StaticDetails.QueryDescription8)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public ActionResult<IEnumerable<TastingDto>> GetCommonTastingsBetweenCustomerAgronomist([FromRoute] int customerId, [FromRoute] int agronomistId, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            IEnumerable<TastingDto> tastingDtos = _mapper.Map<IEnumerable<TastingDto>>
                (_unitOfWork.CustomerRepo.GetCommonTastingsBetweenCustomerAgronomist(customerId, agronomistId, since, until));
            return Ok(tastingDtos);
        }
        [HttpGet("{customerId:int}/feedbacks-by-months/totals")]
        [SwaggerOperation(Summary = "10 Query (Description below)", Description = StaticDetails.QueryDescription10)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public ActionResult<Dictionary<int, int>> GetTotalFeedbacksByMonths([FromRoute] int customerId, [FromQuery] DateTime since, [FromQuery] DateTime until) 
        {
            return Ok(_unitOfWork.CustomerRepo.GetTotalFeedbacksByMonths(customerId, since, until));
        }
        [HttpGet("{customerId:int}/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public ActionResult<IEnumerable<OrderDto>> GetOrders(int customerId)
        {
            IEnumerable<OrderDto> orders = _mapper.Map<IEnumerable<OrderDto>>(_unitOfWork.OrderRepo.GetAll(o => o.CustomerId == customerId));
            return Ok(orders);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [TypeFilter(typeof(CustomerUpsertFilterAttribute))]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerUpsertDto customerDto) 
        {
            Customer customer = new Customer() 
            {
                Name = customerDto.Name
            };
            await _unitOfWork.CustomerRepo.InsertAsync(customer);
            await _unitOfWork.Save();
            await _unitOfWork.CustomerTastingRepo
                .InsertRangeAsync((customerDto.TastingIds ?? Array.Empty<int>())
                .Distinct()
                .Select(i => new CustomerTastings { CustomerId = customer.Id, TastingId = i }));
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new {customerId = customer.Id}, _mapper.Map<CustomerDto>(customer));
        }
        [HttpPut("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(CustomerUpsertFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] CustomerUpsertDto customerDto) 
        {
            Customer customer = new Customer() 
            {
                Id = customerId,
                Name = customerDto.Name,
            };
            _unitOfWork.CustomerRepo.Update(customer);
            await _unitOfWork.Save();
            _unitOfWork.CustomerTastingRepo.DeleteRange(_unitOfWork.CustomerTastingRepo.GetAll(ct => ct.CustomerId == customerId));
            await _unitOfWork.CustomerTastingRepo
                .InsertRangeAsync((customerDto.TastingIds ?? Array.Empty<int>())
                .Distinct()
                .Select(i => new CustomerTastings { CustomerId = customerId, TastingId = i }));
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int customerId) 
        {
            Customer customer = (Customer)HttpContext.Items["customer"]!;
            _unitOfWork.OrderRepo.DeleteRange(_unitOfWork.OrderRepo.GetAll(o => o.CustomerId == customerId));
            _unitOfWork.ReturnRepo.DeleteRange(_unitOfWork.ReturnRepo.GetAll(r => r.CustomerId == customerId));
            _unitOfWork.CustomerRepo.Delete(customer);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
