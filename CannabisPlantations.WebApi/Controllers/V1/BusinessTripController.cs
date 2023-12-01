using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Models.Dtos;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.BusinessTripActionFilters;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BusinessTripController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BusinessTripController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BusinessTripDto>> GetAll()
        {
            IEnumerable<BusinessTripDto> businessTrips = _mapper
                .Map<IEnumerable<BusinessTripDto>>
                (_unitOfWork.BusinessTripRepo.GetAll());
            return Ok(businessTrips);
        }
        [HttpGet("{businessTripId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(BusinessTripExistFilterAttribute))]
        public ActionResult<BusinessTripDto> Get([FromRoute] int businessTripId)
        {
            BusinessTripDto businessTripDto = _mapper
                .Map<BusinessTripDto>
                (HttpContext.Items["businessTrip"]);
            return Ok(businessTripDto);
        }
        [HttpGet("{businessTripId:int}/agronomists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(BusinessTripExistFilterAttribute))]
        public ActionResult<AgronomistDto> GetAgronomists([FromRoute] int businessTripId) 
        {
            IEnumerable<AgronomistDto> agronomistDtos = _mapper
                .Map<IEnumerable<AgronomistDto>>
                (_unitOfWork.AgronomistBusinessTripRepo.GetAgronomists(businessTripId));
            return Ok(agronomistDtos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [BusinessTripDateFilter]
        [TypeFilter(typeof(BusinessTripUpsertFilterAttribute))]
        public async Task<ActionResult<BusinessTripDto>> Create([FromBody] BusinessTripUpsertDto businessTripDto)
        {
            BusinessTrip businessTrip = new BusinessTrip()
            {
                StartDate = (DateTime)businessTripDto.StartDate!,
                EndDate = businessTripDto.EndDate,
            };
            await _unitOfWork.BusinessTripRepo.InsertAsync(businessTrip);
            await _unitOfWork.Save();
            await _unitOfWork.AgronomistBusinessTripRepo
                .InsertRangeAsync((businessTripDto.AgronomistIds ?? Array.Empty<int>())
                .Distinct()
                .Select(i => new AgronomistBusinessTrips { AgronomistId = i, BusinessTripId = businessTrip.Id }));
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new { businessTripId = businessTrip.Id }, _mapper.Map<BusinessTripDto>(businessTrip));
        }
        [HttpPut("{businessTripId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(BusinessTripExistFilterAttribute))]
        [BusinessTripDateFilter]
        [TypeFilter(typeof(BusinessTripUpsertFilterAttribute))]
        public async Task<IActionResult> Update([FromRoute] int businessTripId, [FromBody] BusinessTripUpsertDto businessTripDto)
        {
            BusinessTrip businessTrip = new BusinessTrip()
            {
                Id = businessTripId,
                StartDate = (DateTime)businessTripDto.StartDate!,
                EndDate = businessTripDto.EndDate
            };
            _unitOfWork.BusinessTripRepo.Update(businessTrip);
            await _unitOfWork.Save();
            _unitOfWork.AgronomistBusinessTripRepo.DeleteRange(_unitOfWork.AgronomistBusinessTripRepo.GetAll(abt => abt.BusinessTripId == businessTrip.Id));
            await _unitOfWork.AgronomistBusinessTripRepo
                .InsertRangeAsync((businessTripDto.AgronomistIds ?? new int[0])
                .Distinct()
                .Select(i => new AgronomistBusinessTrips { AgronomistId = i, BusinessTripId = businessTrip.Id }));
            await _unitOfWork.Save();
            return NoContent();
        }
        [HttpDelete("{businessTripId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(BusinessTripExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int businessTripId) 
        {
            BusinessTrip businessTrip = (BusinessTrip)HttpContext.Items["businessTrip"]!;
            _unitOfWork.BusinessTripRepo.Delete(businessTrip);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
