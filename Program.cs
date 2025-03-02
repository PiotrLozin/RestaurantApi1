using RestaurantApi;
using RestaurantApi.Entities;
using RestaurantApi.WeatherForecast.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Tworzy obiekt za ka¿dym razem, gdy jest potrzebny - czyli nawet kilka razy na jedno zapytanie
builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddTransient<RestaurantSeeder>();
builder.Services.AddDbContext<RestaurantDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<RestaurantSeeder>();
    seeder.Seed();
}

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
