using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emids.QA.Application.Common.Config
{
    public class ModelValidationErrorHandlerFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var sb = new StringBuilder();

                foreach (var key in context.ModelState.Keys)
                {
                    if (string.IsNullOrEmpty(context.ModelState[key].AttemptedValue))
                    {
                        sb.Append(context.ModelState[key].Errors.Select(p => p.ErrorMessage).FirstOrDefault()).Append(", ");
                    }
                }
                var errorMessages = sb.ToString();
                errorMessages = errorMessages.Remove(errorMessages.LastIndexOf(','), 1);
                //422 Unprocessable Entity Explained
                context.Result = new ObjectResult(errorMessages) { StatusCode = 422 };
            }
            else
            {
                await next();
            }
        }
    }
}
