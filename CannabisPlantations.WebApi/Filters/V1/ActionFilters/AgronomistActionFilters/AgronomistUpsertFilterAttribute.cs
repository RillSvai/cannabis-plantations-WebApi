using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.AgronomistActionFilters
{
    public class AgronomistUpsertFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public AgronomistUpsertFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            AgronomistUpsertDto? agronomistDto = context.ActionArguments["agronomistDto"] as AgronomistUpsertDto;
            if (agronomistDto is null)
            {
                context.ModelState.AddModelError("Agronomist", "Object is null.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (agronomistDto!.BusinessTripIds is null || agronomistDto.BusinessTripIds.Length == 0) 
            {
                await next();
                return;
            }            
            if (agronomistDto.BusinessTripIds.Any(i => i <= 0))
            {
                context.ModelState.AddModelError("BusinessTripId", "At least one business trip`s id is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (agronomistDto.BusinessTripIds.Any(i => _unitOfWork.BusinessTripRepo.Get(bt => bt.Id == i) is null)) 
            {
                context.ModelState.AddModelError("Businesss trip", "At least one business trip doesn`t exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            await next();
        }
    }
}
