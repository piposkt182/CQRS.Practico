using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.ApplicationDbContext;
using MyApp.Infrastructure.DependencyInjections; // Newly added

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Centralized service registrations
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

// Configure DbContext (assuming AppDbContext is from MyApp.Infrastructure.Context)
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));

// MediatR registration
builder.Services.AddMediatR(typeof(Program).Assembly);



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
