using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockTrackingApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSqlite<ApplicationDbContext>(builder.Configuration.GetConnectionString("Sqlite"))
    .AddDbContext<ApplicationDbContext>();


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
