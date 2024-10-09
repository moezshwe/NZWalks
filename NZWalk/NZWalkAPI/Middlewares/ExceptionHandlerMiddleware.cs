using System.Net;

namespace NZWalkAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)

        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                var errorID = Guid.NewGuid();
                logger.LogError(e, $"{errorID}:{e.Message}");
                context.Response .StatusCode =(int)HttpStatusCode .InternalServerError;
                context.Response.ContentType = "application/json";
                var error = new
                {
                    id = errorID,
                    ErrorMessage = "Something went wrong! We are looking into resolving this."
                };

                await context.Response.WriteAsJsonAsync(error);

            }

        }
    }
}
