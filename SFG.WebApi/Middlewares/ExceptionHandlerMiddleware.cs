using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using SFG.Core.Commons;
using SFG.WebApi.Model;

namespace SFG.WebApi.Middlewares
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(app =>
            {
                app.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var errorCustom = context.RequestServices.GetService<ILogger<CustomExceptionHandler>>();
                    if (exceptionHandlerFeature != null)
                    {
                        var error = new ErrorMessage
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = exceptionHandlerFeature.Error.Message
                        };

                        if(exceptionHandlerFeature.GetType() == typeof(CustomExceptionHandler))
                        {
                            error.Message = ((CustomExceptionHandler)errorCustom).ErrorMessage;
                        }
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                    }
                });
            });

            return app;
        }
    }
}

