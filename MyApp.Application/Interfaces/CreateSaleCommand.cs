
using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public class CreateSaleCommand
    {
        public List<SaleItemDto> Items { get; set; }
    }
}
