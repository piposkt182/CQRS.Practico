using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Language", Schema = "dbo")]
    public class Lenguaje
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
