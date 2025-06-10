using MyApp.Application.Interfaces;

namespace MyApp.Application.Commands
{
    public class CreateProductCommand : ICommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
