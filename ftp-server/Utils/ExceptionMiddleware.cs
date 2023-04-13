using System.Net;
using System.Text.Json;

namespace ftp_server.Utils
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ErrorDetails? errorDetails = null;

                if (ex is ArgumentNullException)
                {
                    errorDetails = SetStatusCodeAndMessage((int)HttpStatusCode.NotFound, ex.Message);
                }
                else if (ex is ArgumentException)
                {
                    errorDetails = SetStatusCodeAndMessage((int)HttpStatusCode.BadRequest, ex.Message);    
                }

                if (errorDetails is null) throw;
                
                await HandleExceptionAsync(context, errorDetails); 
            }
        }

        private Task HandleExceptionAsync(HttpContext context, ErrorDetails errorDetails)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDetails.StatusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }

        private ErrorDetails SetStatusCodeAndMessage(int statusCode, string message) =>
            new ErrorDetails
            {
                StatusCode = statusCode,
                Message = message,
            };
    }
}
