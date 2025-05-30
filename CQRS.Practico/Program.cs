using Autofac.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program).Assembly);
//builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TaskDb"));

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));


builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>, GetAllTicketsHandler>();
builder.Services.AddScoped<ICommandHandler<CreateTicketCommand>, CreateTicketHandler>();
builder.Services.AddScoped<ICommandHandler<CreateSaleCommand>, CreateSaleHandler>();
builder.Services.AddTransient<IQueryHandler<GetTicketByIdQuery, TicketDto>, GetTicketByIdHandler>();


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
