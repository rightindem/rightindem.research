using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Commands
{
    public class StartEngineCommand : ICommand {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
    }
}