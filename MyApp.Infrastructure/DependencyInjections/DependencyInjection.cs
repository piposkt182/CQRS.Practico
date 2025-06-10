using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Repositories;
using MyApp.Application.Commands;   
using MyApp.Application.Queries;
using MyApp.Application.Validators; 
using FluentValidation;
using MyApp.Application.DTOs;
using MyApp.Application.Handlers.CommandHandlers; 
using MyApp.Application.Handlers.QueryHandlers;  
using MyApp.Domain.Entities; 
using MediatR;

namespace MyApp.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) // Removed IConfiguration config
        {
            // AppDbContext 
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddTransient<IProductRepository, ProductRepository>(); 
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>(); 
            services.AddScoped<ILanguageRepository, LanguageRepository>(); 
            services.AddScoped<IMovieRepository, MovieRepository>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Validators
            services.AddTransient<IValidator<UpdateTicketCommand>, UpdateTicketCommandValidator>();

            // Register Query Handlers
            services.AddScoped<IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>, GetAllTicketsHandler>();
            services.AddTransient<IRequestHandler<GetTicketByIdQuery, TicketDto>, GetTicketByIdHandler>();
            services.AddTransient<IRequestHandler<GetTimbradoTicketsQuery, IEnumerable<TicketDto>>, GetTimbradoTicketsHandler>();
            // Add this line for the new gender query handler
            services.AddScoped<IQueryHandler<GetAllGendersQuery, IEnumerable<GenderDto>>, GetAllGendersHandler>();

            // Register Command Handlers
            services.AddScoped<ICommandHandler<CreateTicketCommand>, CreateTicketHandler>();
            services.AddTransient<ICommandHandler<UpdateTicketCommand>, UpdateTicketHandler>(); 
            services.AddScoped<ICommandHandler<CreateSaleCommand>, CreateSaleHandler>();
            services.AddTransient<ICommandHandler<DeleteTicketCommand>, DeleteTicketCommandHandler>();
            services.AddTransient<ICommandHandler<CreateGenderCommand>, CreateGenderCommandHandler>(); 

            // MediatR style Handlers for Lenguaje (assuming MediatR will be fully integrated)
            services.AddTransient<MediatR.IRequestHandler<CreateLenguajeCommand, LenguajeDto>, CreateLenguajeCommandHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetAllLenguajesQuery, IEnumerable<LenguajeDto>>, GetAllLenguajesQueryHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetLenguajeByIdQuery, LenguajeDto>, GetLenguajeByIdQueryHandler>();

            // Movie Handlers
            services.AddTransient<MediatR.IRequestHandler<CreateMovieCommand, int>, CreateMovieCommandHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>, GetAllMoviesQueryHandler>();
            services.AddTransient<MediatR.IRequestHandler<GetMovieByIdQuery, MovieDto>, GetMovieByIdQueryHandler>();

            // Registrations for Product
            services.AddTransient<IRequestHandler<CreateProductCommand, ProductDto>, CreateProductCommandHandler>();
            services.AddTransient<IRequestHandler<GetProductByIdQuery, ProductDto>, GetProductByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>, GetAllProductsQueryHandler>();
            
            return services;
        }
    }
}
