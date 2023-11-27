using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters
{
    public class IdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            int[] ids = context.ActionArguments.Select(pair => pair.Value).OfType<int>().ToArray();
            if (ids.Length > 0 && ids.Any(id => id <= 0)) 
            {
                context.ModelState.AddModelError("", "At least one id is not valid.");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
            }         
        }
    }
}
