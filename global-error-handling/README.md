# Features
- Global Error Handling for Dev & Prod env

```
http://localhost:5000/WeatherForecast/Redmond -> success
http://localhost:5000/WeatherForecast/chicago -> exception

Exception will different for Dev & Prod env
```

## Approach 1

```cs
// Startup.cs
if (env.IsDevelopment())
{
    app.UseExceptionHandler("/error-local-development");
}
else
{
    app.UseExceptionHandler("/error");
}
```

## Approach 2

```cs
// Startup.cs
app.UseExceptionHandler(a => a.Run(async context =>
{
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Response.ContentType = "application/json";

    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = feature.Error;

    var result = JsonSerializer.Serialize(new { error = exception.Message });
    // var result = new ErrorDetails()
    // {
    //     StatusCode = context.Response.StatusCode,
    //     Message = "Internal Server Error."
    // }.ToString();
    await context.Response.WriteAsync(result);
}));
```

## Approach 3: Using custom Middleware (Recommended)

```cs
// ExceptionMiddleware.cs
// Startup.cs
app.UseMiddleware<ExceptionMiddleware>();
```

# References
- https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-3.1
- https://code-maze.com/global-error-handling-aspnetcore/
- https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling