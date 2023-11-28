using AutoMapper;
using CannabisPlantations.WebApi.Data;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.OrderActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderDto>> GetAll() 
        {
            IEnumerable<OrderDto> orderDtos = _mapper.Map<IEnumerable<OrderDto>>(_unitOfWork.OrderRepo.GetAll());
            return Ok(orderDtos);
        }
        [HttpGet("{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(OrderExistActionFilterAttribute))]
        public ActionResult<OrderDto> Get([FromRoute] int orderId) 
        {
            OrderDto orderDto = _mapper.Map<OrderDto>(HttpContext.Items["order"]);
            return Ok(orderDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderUpsertDto orderDto, [FromQuery] int agronomistId, [FromQuery] int customerId) 
        {
            Order order = new Order()
            {
                Date = (DateTime)orderDto.Date!,
                AgronomistId = agronomistId,
                CustomerId = customerId
            };
            await _unitOfWork.OrderRepo.InsertAsync(order);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new {orderId = order.Id}, _mapper.Map<OrderDto>(order));
        }
        [HttpPut("{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(CustomerExistFilterAttribute))]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
        [TypeFilter(typeof(OrderExistActionFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int orderId, [FromBody] OrderUpsertDto orderDto, [FromQuery] int agronomistId, [FromQuery] int customerId) 
        {
            Order order = new Order() 
            {
                Id = orderId,
                Date = (DateTime)orderDto.Date!,
                AgronomistId = agronomistId,
                CustomerId = customerId
            };   
            _unitOfWork.OrderRepo.Update(order);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
