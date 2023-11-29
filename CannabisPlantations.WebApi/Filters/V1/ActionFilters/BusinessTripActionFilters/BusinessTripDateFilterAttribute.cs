using CannabisPlantations.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.BusinessTripActionFilters
{
    public class BusinessTripDateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            BusinessTripUpsertDto? businessTripDto = context.ActionArguments["businessTripDto"] as BusinessTripUpsertDto;  
            if ((businessTripDto?.EndDate ?? DateTime.MaxValue) < ((DateTime)businessTripDto!.StartDate!).AddHours(1))
            {
                context.ModelState.AddModelError("Date", "End date shouldn`t be less than start date and greater at least on 1 hour.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
            }
        }
    }
}
