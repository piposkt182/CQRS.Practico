namespace MyApp.Application.Interfaces
{
    public class GetTimbradoTicketsQuery
    {
        public bool Timbrado { get; }

        public GetTimbradoTicketsQuery(bool timbrado)
        {
            Timbrado = timbrado;
        }
    }
}
