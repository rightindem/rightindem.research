using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class IgnitionStartFailedEvent: IFailedEvent
    {
        public string Message { get; }
        public IgnitionStartFailedEvent(string message)
        {
            Message = message;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}