using Microsoft.EntityFrameworkCore;
using WeddingManager.Infraestructure.Persistence;
using WeddingManager.Application.Interfaces;
using WeddingManager.Infraestructure.Repositories;
using WeddingManager.Application.Services;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using WeddingManager.Infraestructure.Storage;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddScoped<IFileStorage, S3FileStorage>();

builder.Services.AddScoped<IWeddingRepository, WeddingRepository>();
builder.Services.AddScoped<IWeddingTaskRepository, WeddingTaskRepository>();

builder.Services.AddScoped<WeddingService>();
builder.Services.AddScoped<WeddingTaskService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AngularPolicy");

app.MapControllers();

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
