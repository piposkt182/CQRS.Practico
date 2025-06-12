
using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public class CreateSaleCommand
    {
        public decimal Total { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }
}
