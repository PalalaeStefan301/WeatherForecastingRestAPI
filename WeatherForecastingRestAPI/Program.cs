using BLL.Abstract;
using BLL.Concrete;
using DAL.Models;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using DAL.Abstract;
using DAL.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("WeatherForecastingRestAPI"));

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IGeocodingService, GeocodingService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();


builder.Host.UseSerilog((context, services, configuration) => configuration
    .Enrich.FromLogContext()
    .WriteTo.File("logs/log_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".txt", rollingInterval: RollingInterval.Hour,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();