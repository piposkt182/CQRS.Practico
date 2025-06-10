using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Repositories;
using MyApp.Application.Commands;   
using MyApp.Application.Queries;    
using MyApp.Application.Validators; 
using FluentValidation;
using MyApp.Application.DTOs;
using System.Collections.Generic;
using MediatR; // Ensured MediatR is used
using MyApp.Application.Handlers.CommandHandlers; // For CreateMovieCommandHandler
using MyApp.Application.Handlers.QueryHandlers;   // For Movie Query Handlers

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
            services.AddScoped<IGenderRepository, GenderRepository>(); // Added
            services.AddScoped<ILanguageRepository, LanguageRepository>(); // Corrected ILenguajeRepository to ILanguageRepository
            services.AddScoped<IMovieRepository, MovieRepository>(); // Added IMovieRepository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Validators
            services.AddTransient<IValidator<UpdateTicketCommand>, UpdateTicketCommandValidator>();

            // Register Query Handlers
            services.AddScoped<IQueryHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>, GetAllTicketsHandler>();
            services.AddTransient<IQueryHandler<GetTicketByIdQuery, TicketDto>, GetTicketByIdHandler>();
            services.AddTransient<IQueryHandler<GetTimbradoTicketsQuery, IEnumerable<TicketDto>>, GetTimbradoTicketsHandler>();
            // Add this line for the new gender query handler
            services.AddScoped<IQueryHandler<GetAllGendersQuery, IEnumerable<GenderDto>>, GetAllGendersHandler>();

            // Register Command Handlers
            services.AddScoped<ICommandHandler<CreateTicketCommand>, CreateTicketHandler>();
            services.AddTransient<ICommandHandler<UpdateTicketCommand>, UpdateTicketHandler>(); // Kept one Transient registration
            services.AddScoped<ICommandHandler<CreateSaleCommand>, CreateSaleHandler>();
            services.AddTransient<ICommandHandler<DeleteTicketCommand>, DeleteTicketCommandHandler>();
            services.AddTransient<ICommandHandler<CreateGenderCommand>, CreateGenderCommandHandler>(); // Added

            // MediatR style Handlers for Lenguaje (assuming MediatR will be fully integrated)
            // If not, these would need to align with custom ICommandHandler/IQueryHandler,
            // which might require changes to CreateLenguajeCommandHandler or ICommandHandler interface.
            services.AddTransient<MediatR.IRequestHandler<CreateLenguajeCommand, LenguajeDto>, CreateLenguajeCommandHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetAllLenguajesQuery, IEnumerable<LenguajeDto>>, GetAllLenguajesQueryHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetLenguajeByIdQuery, LenguajeDto>, GetLenguajeByIdQueryHandler>();

            // Movie Handlers
            services.AddTransient<MediatR.IRequestHandler<CreateMovieCommand, int>, CreateMovieCommandHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>, GetAllMoviesQueryHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetMovieByIdQuery, MovieDto>, GetMovieByIdQueryHandler>();

            return services;
        }
    }
}
