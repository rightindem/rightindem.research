using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class UnlockFailedEvent : IEvent
    {
        public string Message { get; }
        public UnlockFailedEvent(string message)
        {
            Message = message;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}