namespace MyApp.Application.Interfaces
{
    // No explicit using needed for ICommand as it's in the same namespace.
    // If ICommand were in a different namespace, e.g., MyApp.Application.Core,
    // then 'using MyApp.Application.Core;' would be required.

    public class UpdateTicketCommand : ICommand
    {
        public int Id { get; set; } // Corresponds to Ticket.Codigo
        public string NombreTicket { get; set; } // Corresponds to Ticket.NombreTicket
        public string DesignTicket { get; set; } // Corresponds to Ticket.DesignTicket
        public bool Timbrado { get; set; } // Corresponds to Ticket.Timbrado
    }
}
