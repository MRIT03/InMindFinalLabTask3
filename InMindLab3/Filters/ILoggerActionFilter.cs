using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

/*
 * This logger uses Ilogger instead of the command line.
 */


namespace InMindLab1.Filters
{
    public class ILoggerActionFilter : IActionFilter
    {
        private readonly ILogger<ILoggerActionFilter> _logger;

        public ILoggerActionFilter(ILogger<ILoggerActionFilter> logger)
        {
            _logger = logger;
        }

        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("[ILoggerActionFilter] Action executing: {ActionName}",
                context.ActionDescriptor.DisplayName);
        }

       
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("[ILoggerActionFilter] Action executed: {ActionName}",
                context.ActionDescriptor.DisplayName);
        }
    }
}