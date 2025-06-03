using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Domain.Entities;
using MyApp.Application.Interfaces; // Required for CreateTicketCommand
using MyApp.Infrastructure.ApplicationDbContext;
using Xunit;

namespace CQRS.Practico.Tests.ControllerTests;

// Custom WebApplicationFactory to override services for testing
public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's AppDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add AppDbContext using an in-memory database for testing.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database contexts.
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                // Seed the database with test data if needed (example below)
                // SeedData(db);
            }
        });
    }

    // Example of seeding data
    // private static void SeedData(AppDbContext context)
    // {
    //     context.Tickets.Add(new Ticket { Id = 1, Title = "Test Ticket 1", Description = "Description 1", CreatedDate = DateTime.UtcNow, IsResolved = false });
    //     context.SaveChanges();
    // }
}

public class TicketsControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly TestingWebAppFactory<Program> _factory;

    public TicketsControllerTests(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(); // Creates an HttpClient that automatically follows redirects and handles cookies
    }

    private async Task<Ticket> SeedTicketAsync(CreateTicketCommand command)
    {
        // Use the AppDbContext from the factory's service provider to add data directly
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var ticket = new Ticket
        {
            Title = command.Title,
            Description = command.Description,
            Contact = command.Contact,
            DateCreated = DateTime.UtcNow, // Assuming DateCreated is set on creation
            IsResolved = false
        };

        context.Tickets.Add(ticket);
        await context.SaveChangesAsync();
        return ticket; // Return the seeded ticket with its generated ID
    }

    [Fact]
    public async Task DeleteTicket_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var createCommand = new CreateTicketCommand("Test Delete Ticket", "This is a ticket to be deleted.", "delete@example.com");
        var seededTicket = await SeedTicketAsync(createCommand); // Seed a ticket and get its ID

        // Act
        var response = await _client.DeleteAsync($"/api/Tickets/{seededTicket.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Optionally, verify it's actually deleted
        var getResponse = await _client.GetAsync($"/api/Tickets/{seededTicket.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteTicket_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = 9999; // An ID that is unlikely to exist

        // Act
        var response = await _client.DeleteAsync($"/api/Tickets/{nonExistentId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTicketById_NonExistentId_ReturnsNotFound()
    {
        // Arrange
        var nonExistentId = 9998;

        // Act
        var response = await _client.GetAsync($"/api/Tickets/{nonExistentId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetTicketById_ExistingId_ReturnsOkAndTicket()
    {
        // Arrange
        var createCommand = new CreateTicketCommand("Test Get Ticket", "This is a ticket to be retrieved.", "get@example.com");
        var seededTicket = await SeedTicketAsync(createCommand);

        // Act
        var response = await _client.GetAsync($"/api/Tickets/{seededTicket.Id}");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var ticketDto = await response.Content.ReadFromJsonAsync<MyApp.Application.Queries.TicketDto>(); // Assuming TicketDto is the return type
        Assert.NotNull(ticketDto);
        Assert.Equal(seededTicket.Id, ticketDto.Id);
        Assert.Equal(seededTicket.Title, ticketDto.Title);
    }
}
