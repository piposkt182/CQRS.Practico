using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;
using MyApp.Infrastructure.Repositories;
using MyApp.Application.Interfaces; // For ICommandHandler, UpdateTicketCommand (as generic arg)
using MyApp.Application.Commands;   // For UpdateTicketHandler
using MyApp.Application.Validators; // For UpdateTicketCommandValidator
using FluentValidation;             // For IValidator

namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

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

            // Register Command Handlers
            services.AddTransient<ICommandHandler<UpdateTicketCommand>, UpdateTicketHandler>();

            // Add other application services registrations here if any

            return services;
        }
    }
}
