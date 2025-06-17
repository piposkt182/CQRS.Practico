
namespace MyApp.Application.DTOs
{
    public record TicketDto(int Codigo, string NombreTicket, string DesignTicket, bool Timbrado, int MovieId, int SaleId);
}
