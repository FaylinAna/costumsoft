using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CUSTOMSOFT.API.Filters
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://example.com/validation-error",
                    Title = "Validation Error",
                    Status = 400,
                    Detail = "Uno o más campos no son válidos.",
                    Instance = context.HttpContext.Request.Path
                };

                foreach (var error in context.ModelState.Values.SelectMany(v => v.Errors))
                {
                    problemDetails.Extensions.Add(error.Exception?.GetType().ToString() ?? "Error", error.ErrorMessage);
                }

                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://example.com/validation-error",
                    Title = "Validation Error",
                    Status = 400,
                    Detail = "Uno o más campos no son válidos.",
                    Instance = context.HttpContext.Request.Path
                };

                foreach (var error in context.ModelState.Values.SelectMany(v => v.Errors))
                {
                    problemDetails.Extensions.Add(error.Exception?.GetType().ToString() ?? "Error", error.ErrorMessage);
                }

                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
