using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class IgnitionStopFailedEvent : IFailedEvent
    {
        public string Message { get; }
        public IgnitionStopFailedEvent(string message)
        {
            Message = message;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}