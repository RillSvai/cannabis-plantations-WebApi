using AutoMapper;
using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters;
using CannabisPlantations.WebApi.Filters.V1.ActionFilters.CannabisTypeActionFilters;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CannabisTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CannabisTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("{cannabisTypeId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [IdFilter]
        [TypeFilter(typeof(CannabisTypeExistFilterAttribute))]
        public ActionResult<CannabisTypeDto> Get(int cannabisTypeId) 
        {
            CannabisTypeDto cannabisTypeDto = _mapper.Map<CannabisTypeDto>(_unitOfWork.CannabisTypeRepo.Get(ct => ct.Id == cannabisTypeId));
            return Ok(cannabisTypeDto);
        }
    }
}
