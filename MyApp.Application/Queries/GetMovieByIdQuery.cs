namespace MyApp.Application.Queries
{
    public class GetMovieByIdQuery
    {
        public int Id { get; set; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}
