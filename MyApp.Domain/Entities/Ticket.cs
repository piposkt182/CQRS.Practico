
namespace MyApp.Domain.Entities
{
    public class Ticket
    {
        public int Codigo { get; private set; }
        public string NombreTicket { get; set; }
        public string DesignTicket { get; set; }
        public bool Timbrado { get; set; }
        public int MovieId { get; private set; }
        public int SaleId { get; private set; }

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

            public TicketBuilder WithSaleId(int saleid)
            {
                _ticket.SaleId = saleid;
                return this;
            }

            public TicketBuilder WithMovieId(int Movieid)
            {
                _ticket.MovieId = Movieid;
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
