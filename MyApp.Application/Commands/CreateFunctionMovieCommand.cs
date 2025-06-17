using MediatR;
using MyApp.Application.DTOs;

namespace MyApp.Application.Commands
{
    public record CreateFunctionMovieCommand (int Codigo, int MovieId, List<ProductToBuyDto> products) : IRequest<BuyDto>;
     
}
