using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Commands
{
    public record UpdateProductStockCommand(int id, int stock) : IRequest<ProductDto>;
    
}
