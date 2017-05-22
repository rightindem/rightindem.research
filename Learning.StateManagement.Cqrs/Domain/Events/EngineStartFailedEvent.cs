using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class EngineStartFailedEvent : IFailedEvent
    {
        public string Message { get; }
        public EngineStartFailedEvent(string message)
        {
            Message = message;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}