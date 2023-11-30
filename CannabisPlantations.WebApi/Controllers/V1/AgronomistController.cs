using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            IEnumerable<AgronomistDto> agronomistDtos = _mapper.Map<IEnumerable<AgronomistDto>>(_unitOfWork.AgronomistRepo.GetAll());
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
            AgronomistDto agronomistDto = _mapper.Map<AgronomistDto>(HttpContext.Items["agronomist"]);
            return Ok(agronomistDto);
        }
        [HttpGet("{agronomistId:int}/min-sales-duration/customers")]
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [TypeFilter(typeof(AgronomistUpsertFilterAttribute))]
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
