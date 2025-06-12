
using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
