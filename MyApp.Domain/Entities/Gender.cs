using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Gender", Schema = "dbo")]
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
