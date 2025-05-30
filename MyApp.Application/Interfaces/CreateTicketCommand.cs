
namespace MyApp.Application.Interfaces
{
    public class CreateTicketCommand
    {
        public int codigo { get; init; }
        public string NombreTicket { get; init; }
        public string DesignTicket { get; init; }
        public bool Timbrado { get; init; }
    }
}
