using CannabisPlantations.WebApi.Data.Repositories.IRepositories;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CannabisPlantations.WebApi.Filters.V1.ActionFilters.FeedbackActionFilters
{
    public class FeedbackExistFilterAttribute : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeedbackExistFilterAttribute(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int? id = context.ActionArguments["feedbackId"] as int?;
            Feedback? feedback = await _unitOfWork.FeedbackRepo.GetAsync(f => f.Id == id);
            if (feedback is null)
            {
                context.ModelState.AddModelError("Feedback", "Feedback doesn`t exist");
                ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState) 
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(details);
                return;
            }
            context.HttpContext.Items["feedback"] = feedback;
            await next();
        }
    }
}
