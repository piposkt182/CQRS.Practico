using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Duration { get; set; }

        public int LanguageId { get; set; }

        public int GenderId { get; set; }

        public DateTime? EndDate { get; set; } // Added EndDate property

        public virtual Language Language { get; set; }

        public virtual Gender Gender { get; set; }
    }
}
