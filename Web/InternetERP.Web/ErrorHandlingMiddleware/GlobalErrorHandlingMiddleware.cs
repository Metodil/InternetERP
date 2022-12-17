namespace InternetERP.Web.ErrorHandlingMiddleware
{
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    using BadRequestException = InternetERP.Web.ErrorHandlingMiddleware.Exceptions.BadRequestException;
    using KeyNotFoundException = InternetERP.Web.ErrorHandlingMiddleware.Exceptions.KeyNotFoundException;
    using NotFoundException = InternetERP.Web.ErrorHandlingMiddleware.Exceptions.NotFoundException;
    using NotImplementedException = InternetERP.Web.ErrorHandlingMiddleware.Exceptions.NotImplementedException;
    using UnauthorizedAccessException = InternetERP.Web.ErrorHandlingMiddleware.Exceptions.UnauthorizedAccessException;

    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> logger;

        public GlobalErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, this.logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new
            {
                error = message,
                stackTrace,
            });
            logger.LogError(exceptionResult);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
