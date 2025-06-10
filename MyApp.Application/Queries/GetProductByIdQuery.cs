using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }
}
