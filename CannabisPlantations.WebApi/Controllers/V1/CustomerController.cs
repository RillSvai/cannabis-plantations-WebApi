using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerUpsertDto customerDto) 
        {
            Customer customer = new Customer() 
            {
                Name = customerDto.Name
            };
            await _unitOfWork.CustomerRepo.InsertAsync(customer);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new {customerId = customer.Id}, _mapper.Map<CustomerDto>(customer));
        }
        [HttpPut("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] CustomerUpsertDto customerDto) 
        {
            Customer customer = new Customer() 
            {
                Id = customerId,
                Name = customerDto.Name,
            };
            _unitOfWork.CustomerRepo.Update(customer);
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
