using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Techtalk.FM.API.Filters
{
    public class APIResultFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Called asynchronously before the action, after model binding is complete and add APIResult to content.
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext.</param>
        /// <param name="next">The Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate. 
        /// Invoked to execute the next action filter or the action itself.</param>
        /// <returns>A System.Threading.Tasks.Task that on completion indicates the filter has executed.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext executedContext = await next();

            if (executedContext.Exception == null)
            {
                executedContext.Result = new OkObjectResult(executedContext.Result);
            }
            else
            {
                if (executedContext.Exception is ArgumentException arg)
                {
                    if (executedContext.HttpContext != null)
                    {
                        executedContext.HttpContext.Items.Add("Exception", arg);
                        executedContext.HttpContext.Items.Add("IsHandledError", true);
                    }

                    BadRequestObjectResult result = new BadRequestObjectResult(arg.Message)
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                    executedContext.Result = result;
                    executedContext.Exception = null;
                }
                else if (executedContext.Exception is Exception ex)
                {
                    if (executedContext.HttpContext != null)
                    {
                        executedContext.HttpContext.Items.Add("Exception", ex);
                        executedContext.HttpContext.Items.Add("IsHandledError", false);
                    }

                    JsonResult result = new JsonResult(ex.Message)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    executedContext.Result = result;
                    executedContext.Exception = null;
                }
            }
        }
    }
}
