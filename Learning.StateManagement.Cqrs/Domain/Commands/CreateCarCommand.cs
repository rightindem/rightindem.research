using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Commands
{
    public class CreateCarCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public string Model { get; set; }
    }
}