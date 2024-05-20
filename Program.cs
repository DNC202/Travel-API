using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.Services.DestinationServices;
using Tour_API.Services.TourServices;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<TourContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<ITourService, TourService>();

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(x => x.Expire(TimeSpan.FromMinutes(10)));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseOutputCache();

app.Run();
