using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RestApi.Tests
{
    internal sealed class ErrorHandlingMiddleware
    {
        private const string MessageFormat = "HTTP {0} {1} responded {2}.";
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                int? statusCode = null;
                if (httpContext.Response != null)
                {

                    if (exception is NotFoundException)
                    {
                        statusCode = httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await httpContext.Response.WriteAsJsonAsync("Error: " + exception.Message);
                    }
                    else
                    {
                        statusCode = httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await httpContext.Response.WriteAsJsonAsync("Error: " + exception.Message);
                    }
                }

                _logger.LogError(exception, MessageFormat, httpContext.Request.Method, GetPath(httpContext), statusCode);
            }
        }

        private string GetPath(HttpContext httpContext)
        {
            return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget ?? httpContext.Request.Path.ToString();
        }
    }
}