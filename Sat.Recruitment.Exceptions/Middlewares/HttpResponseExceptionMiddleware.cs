using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using Sat.Recruitment.Exceptions.Common;
using Sat.Recruitment.Exceptions.Models;

namespace Sat.Recruitment.Exceptions.Middlewares
{
    public class HttpResponseExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger _logger;

        public HttpResponseExceptionMiddleware(RequestDelegate next, IHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
            _logger = LogManager.GetCurrentClassLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public async Task Invoke(HttpContext context)
        {
            HttpResponseError response = null;

            try
            {
                await _next.Invoke(context);
            }
            catch (HttpResponseException ex)
            {
                response = new HttpResponseError(status: ex.StatusCode, message: ex.Message);
                if (_environment.IsDevelopment())
                {
                    response.Error = ex.Error;
                    response.Trace = ex.StackTrace;
                }

                _logger.Error($"HttpResponseException endpoint {context.Request.Method}: {context.Request.Path.Value} {ex.Error} {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                response = new HttpResponseError(status: HttpStatusCode.InternalServerError, message: ex.Message);
                if (_environment.IsDevelopment())
                {
                    response.Trace = ex.StackTrace;
                }

                _logger.Error($"Exception endpoint {context.Request.Method}: {context.Request.Path} {ex.Message} {ex.StackTrace}");

                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    _logger.Error($"InnerException: {innerException.Message} {innerException.StackTrace}");
                    innerException = innerException.InnerException;
                }
            }
            finally
            {
                if (!context.Response.HasStarted && response != null && response.StatusCode != HttpStatusCode.NoContent)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = response.StatusCode.GetValue();

                    string jsonResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });

                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }
    }

    public static class HttpResponseExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpResponseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpResponseExceptionMiddleware>();

            return app;
        }
    }
}
