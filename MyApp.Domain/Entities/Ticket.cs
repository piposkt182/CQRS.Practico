
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Domain.Entities
{
    [Table("Ticket", Schema = "dbo")]
    public class Ticket
    {
        public int Codigo { get; private set; }
        public string NombreTicket { get; private set; }
        public string DesignTicket { get; private set; }
        public bool Timbrado { get; private set; }

        private Ticket() { }

        public static TicketBuilder CreateBuilder() => new TicketBuilder();

        public class TicketBuilder
        {
            private readonly Ticket _ticket = new();

            public TicketBuilder WithCodigoTicket(int codigo)
            {
                _ticket.Codigo = codigo;
                return this;
            }
            public TicketBuilder WithNombreTicket(string nombre)
            {
                _ticket.NombreTicket = nombre;
                return this;
            }

            public TicketBuilder WithDesignTicket(string diseño)
            {
                _ticket.DesignTicket = diseño;
                return this;
            }

            public TicketBuilder WithTimbrado(bool timbrado)
            {
                _ticket.Timbrado = timbrado;
                return this;
            }

            public Ticket Build()
            {
                if(_ticket.Codigo == null || _ticket.Codigo == 0)
                    throw new ArgumentException("El Codigo del ticket es obligatorio");

                if (string.IsNullOrWhiteSpace(_ticket.NombreTicket))
                    throw new ArgumentException("El nombre del ticket es obligatorio");

                if (string.IsNullOrWhiteSpace(_ticket.DesignTicket))
                    throw new ArgumentException("El diseño del ticket es obligatorio");

                return _ticket;
            }
        }
    }
}
