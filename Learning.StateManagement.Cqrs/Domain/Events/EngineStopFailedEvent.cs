using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class EngineStopFailedEvent : IFailedEvent
    {
        public string Message { get; }

        public EngineStopFailedEvent(string message)
        {
            Message = message;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}