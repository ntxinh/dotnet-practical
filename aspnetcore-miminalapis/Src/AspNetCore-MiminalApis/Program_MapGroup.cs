// using AspNetCoreMiminalApis;
// using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>();
// // builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // Minimal APIs with MapGroup
// var todoItems = app.MapGroup("/todoitems");

// todoItems.MapGet("/", async (ApplicationDbContext db) =>
//     await db.Todos.ToListAsync());

// todoItems.MapGet("/complete", async (ApplicationDbContext db) =>
//     await db.Todos.Where(t => t.IsComplete).ToListAsync());

// todoItems.MapGet("/{id}", async (int id, ApplicationDbContext db) =>
//     await db.Todos.FindAsync(id)
//         is Todo todo
//             ? Results.Ok(todo)
//             : Results.NotFound());

// todoItems.MapPost("/", async (Todo todo, ApplicationDbContext db) =>
// {
//     db.Todos.Add(todo);
//     await db.SaveChangesAsync();

//     return Results.Created($"/todoitems/{todo.Id}", todo);
// });

// todoItems.MapPut("/{id}", async (int id, Todo inputTodo, ApplicationDbContext db) =>
// {
//     var todo = await db.Todos.FindAsync(id);

//     if (todo is null) return Results.NotFound();

//     todo.Name = inputTodo.Name;
//     todo.IsComplete = inputTodo.IsComplete;

//     await db.SaveChangesAsync();

//     return Results.NoContent();
// });

// todoItems.MapDelete("/{id}", async (int id, ApplicationDbContext db) =>
// {
//     if (await db.Todos.FindAsync(id) is Todo todo)
//     {
//         db.Todos.Remove(todo);
//         await db.SaveChangesAsync();
//         return Results.NoContent();
//     }

//     return Results.NotFound();
// });

// // Default Minimal APIs
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
