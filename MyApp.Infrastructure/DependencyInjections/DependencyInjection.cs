using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;
using MyApp.Application.Interfaces; // For ICommandHandler, UpdateTicketCommand (as generic arg)
using MyApp.Application.Commands;   // For UpdateTicketHandler, CreateTicketHandler, CreateSaleHandler, DeleteTicketCommandHandler
using MyApp.Application.Queries;    // For GetAllTicketsHandler, GetTicketByIdHandler, TicketDto, GetAllTicketsQuery, GetTicketByIdQuery
using MyApp.Application.Validators; // For UpdateTicketCommandValidator
using FluentValidation;             // For IValidator
using System.Collections.Generic;   // For IEnumerable

namespace MyApp.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) // Removed IConfiguration config
        {
            // AppDbContext registration removed from here, will be handled in Program.cs
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // UnitOfWork is part of Infrastructure in this setup
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Validators
            services.AddTransient<IValidator<UpdateTicketCommand>, UpdateTicketCommandValidator>();

            // Register Query Handlers
            services.AddScoped<IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>, GetAllTicketsHandler>();
            services.AddTransient<IQueryHandler<GetTicketByIdQuery, TicketDto>, GetTicketByIdHandler>();

            // Register Command Handlers
            services.AddScoped<ICommandHandler<CreateTicketCommand>, CreateTicketHandler>();
            services.AddTransient<ICommandHandler<UpdateTicketCommand>, UpdateTicketHandler>(); // Kept one Transient registration
            services.AddScoped<ICommandHandler<CreateSaleCommand>, CreateSaleHandler>();
            services.AddTransient<ICommandHandler<DeleteTicketCommand>, DeleteTicketCommandHandler>();


            return services;
        }
    }
}
