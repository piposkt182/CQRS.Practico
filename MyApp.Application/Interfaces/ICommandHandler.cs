﻿
namespace MyApp.Application.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
