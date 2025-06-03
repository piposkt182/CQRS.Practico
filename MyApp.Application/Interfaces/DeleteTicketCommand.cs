namespace MyApp.Application.Interfaces;

public record DeleteTicketCommand(int Id) : ICommand;
