
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("Sale", Schema = "dbo")]
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        //public List<SaleItem> Items { get; set; } = new();
    }
}
