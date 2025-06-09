using System;

namespace MyApp.Application.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string LanguageName { get; set; }
        public string GenderName { get; set; }
        public DateTime? EndDate { get; set; } // Added EndDate property
    }
}
