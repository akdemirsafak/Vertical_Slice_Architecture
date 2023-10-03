using Microsoft.AspNetCore.Diagnostics;
using Vertical_Slice_Architecture.Exceptions;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Middlewares;

public static class UseCustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                //bu interfaceden fırlatılan exception u yakalıyoruz.
                var statusCode = exceptionFeature.Error switch
                {
                    ClientSideException => 400,
                    _ => 500
                };

                context.Response.StatusCode = statusCode;
                var response = AppResponse<NoContentResponse>.Fail(exceptionFeature.Error.Message, statusCode);
                await context.Response.WriteAsJsonAsync(response);

            });
        });
    }
}
