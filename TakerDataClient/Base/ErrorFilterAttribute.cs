using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WebTakerData.Base
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<Controller> _logger;

        public ErrorFilterAttribute(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(default(EventId), context.Exception, context.Exception.Message);
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(context.Exception);
            base.OnException(context);
        }
    }
}
