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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AgronomistDto>> Create([FromBody] AgronomistUpsertDto agronomistDto)
        {
            Agronomist agronomist = new Agronomist() 
            {
                Name = agronomistDto.Name,
                IsAvailable = agronomistDto.IsAvailable
            };
            await _unitOfWork.AgronomistRepo.InsertAsync(agronomist);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(Get), new {agronomistId = agronomist.Id}, _mapper.Map<AgronomistDto>(agronomist));
        }
        [HttpPut("{agronomistId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(AgronomistExistFilterAttribute))]
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
            return NoContent();
        }
    }
}
