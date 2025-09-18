using graphql_back.Data;
using graphql_back.Entities;
using graphql_back.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// GraphQL
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("BooksDb"));
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000") // React app URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Enable CORS
app.UseCors("AllowReactApp");

// Seed GraphQL data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new Book { Id = 1, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", PublishedYear = 1999 },
            new Book { Id = 2, Title = "Clean Code", Author = "Robert C. Martin", PublishedYear = 2008 },
            new Book { Id = 3, Title = "Design Patterns", Author = "Erich Gamma et al.", PublishedYear = 1994 },
            new Book { Id = 4, Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", PublishedYear = 2009 },
            new Book { Id = 5, Title = "Domain-Driven Design", Author = "Eric Evans", PublishedYear = 2003 }
        );
        context.SaveChanges();
    }
}


// Add GraphQL to the application
app.MapGraphQL();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
