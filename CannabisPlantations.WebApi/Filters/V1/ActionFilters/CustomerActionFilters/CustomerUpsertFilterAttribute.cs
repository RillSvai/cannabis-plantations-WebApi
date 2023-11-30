using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.CustomerActionFilters
{
    public class CustomerUpsertFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerUpsertFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            CustomerUpsertDto? customerDto = context.ActionArguments["customerDto"] as CustomerUpsertDto;
            if (customerDto is null)
            {
                context.ModelState.AddModelError("Customer", "Object is null.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (customerDto.TastingIds is null || customerDto.TastingIds.Length == 0)
            {
                await next();
                return;
            }
            if (customerDto.TastingIds.Any(i => i <= 0))
            {
                context.ModelState.AddModelError("TastingId", "At least one tasting`s id is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
                return;
            }
            if (customerDto.TastingIds.Any(i => _unitOfWork.TastingRepo.Get(t => t.Id == i) is null))
            {
                context.ModelState.AddModelError("Tasting", "At least one tasting doesn`t exist.");
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
