using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
