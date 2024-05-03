using CUSTOMSOFT.API.Models;

namespace CUSTOMSOFT.API.Filters
{

    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestDateTime = DateTime.UtcNow;
            var requestMethod = context.Request.Method;
            var requestPath = context.Request.Path;

            string requestBody = null;
       
                requestBody = await FormatRequest(context.Request);
      
            var originalResponseBody = context.Response.Body;

            try
            {
                await _next(context);


                if (context.Response.StatusCode != 200 || context.Response.StatusCode >= 400)
                {
                    var responseStatusCode = context.Response.StatusCode.ToString();
                    var responseBody = await FormatResponse(context.Response);

                    var logEntry = new RequestResponseLog(requestDateTime, requestMethod, requestPath, responseStatusCode, requestBody, responseBody);
                    _logger.LogError(logEntry.ToString());
                }

            }
            finally
            {
                context.Response.Body = originalResponseBody;
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }

}
