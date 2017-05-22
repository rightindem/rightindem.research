using System;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface ICommand
    {
        Guid Id { get; set; }
        Guid AggregateId { get; set; }
    }
}