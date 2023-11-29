using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ProductActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.ReturnActionFilters;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ReturnController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ReturnDto>> GetAll()
        {
            IEnumerable<ReturnDto> returnDtos = _mapper.Map<IEnumerable<ReturnDto>>(_unitOfWork.ReturnRepo.GetAll());
            return Ok(returnDtos);
        }
        [HttpGet("{returnId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(ReturnExistFilterAttribute))]
        public ActionResult<ReturnDto> Get([FromRoute] int returnId)
        {
            ReturnDto returnDto = _mapper.Map<ReturnDto>(HttpContext.Items["return"]);
            return Ok(returnDto);
        }
    }
}
