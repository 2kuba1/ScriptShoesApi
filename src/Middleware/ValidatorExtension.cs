using FluentValidation;

namespace ScriptShoesAPI.Middleware;

public static class ValidatorExtension
{
    public static RouteHandlerBuilder WithValidator<T>(this RouteHandlerBuilder builder)
        where T : class
    {
        builder.Add(endpointBuilder =>
        {
            var originalDelegate = endpointBuilder.RequestDelegate;
            endpointBuilder.RequestDelegate = async context =>
            {
                var validator = context.RequestServices.GetRequiredService<IValidator<T>>();
                context.Request.EnableBuffering();
                var body = await context.Request.ReadFromJsonAsync<T>();

                if (body == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Couldn't map body to request model");
                    return;
                }
                
                var validationResult = await validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(validationResult.Errors);
                    return;
                }

                context.Request.Body.Position = 0;
                await originalDelegate?.Invoke(context)!;
            };
        });

        return builder;
    }
}