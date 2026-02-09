using Microsoft.EntityFrameworkCore;
using DotnetAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// This is the Dependency Injection (DI) container (jjh)
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// injecting dbcontext
builder.Services.AddDbContext<TodoDbContext>(opt => 
    opt.UseInMemoryDatabase("TodoList")
    .UseAsyncSeeding(async (context, created, cancellation) =>
    {
        await context.Set<TodoItem>().AddRangeAsync(new[]
        {
            new TodoItem {Id = 1, Name = "first_task", IsComplete=false},
            new TodoItem {Id = 2, Name = "second_task", IsComplete=false},
            new TodoItem {Id = 3, Name = "third_task", IsComplete=false}
        }, cancellation);

        await context.SaveChangesAsync(cancellation);
    })
);
//add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
// environment is production by default
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

//minimal api example (doesn't use controller)
app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/health", () =>
{
    return "OK";
}).WithName("Healthcheck");

app.MapGet("/old", () =>
{
    return "this is old";
}).WithName("NewEndpoint");

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
