using Microsoft.AspNetCore.Mvc.Filters;
using NHibernate;
using System.Threading.Tasks;

namespace Techtalk.FM.API.Filters
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly ITransaction _transaction;

        public UnitOfWorkFilter(ITransaction transaction)
        {
            _transaction = transaction;
        }

        /// <summary>
        /// Called asynchronously before the action, use UnitOfWork to transaction Commit or Rollback
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
                await _transaction.CommitAsync();
            }
            else
            {
                await _transaction.RollbackAsync();
            }
        }
    }
}
