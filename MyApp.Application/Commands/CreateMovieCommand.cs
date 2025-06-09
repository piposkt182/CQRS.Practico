using MediatR; // Added for IRequest
using System;
// Removed using MyApp.Application.Interfaces; as ICommand might not be needed if IRequest is used

namespace MyApp.Application.Commands
{
    public class CreateMovieCommand : IRequest<int> // Implements IRequest<int>
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public int LanguageId { get; set; }
        public int GenderId { get; set; }
        public DateTime? EndDate { get; set; } // Added EndDate property
    }
}
