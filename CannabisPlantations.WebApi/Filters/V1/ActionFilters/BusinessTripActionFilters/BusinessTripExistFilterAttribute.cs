using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.BusinessTripActionFilters
{
    public class BusinessTripExistFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessTripExistFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? id = context.ActionArguments["businessTripId"] as int?;
            BusinessTrip? businessTrip = await _unitOfWork.BusinessTripRepo.GetAsync(bt => bt.Id == id);
            if (businessTrip is null)
            {
                context.ModelState.AddModelError("Business Trip", "Business Trip doesn`t exist.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            context.HttpContext.Items["businessTrip"] = businessTrip;
            await next();
        }
    }
}
