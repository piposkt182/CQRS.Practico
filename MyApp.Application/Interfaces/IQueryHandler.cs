﻿
namespace MyApp.Application.Interfaces
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
