using MediatR;

namespace MyApp.Application.Commands
{
    public class CreateMovieCommand : IRequest<int> 
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public int LanguageId { get; set; }
        public int GenderId { get; set; }
        public DateTime? EndDate { get; set; } 
    }
}
