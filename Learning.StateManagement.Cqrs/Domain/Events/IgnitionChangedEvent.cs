using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class IgnitionChangedEvent: IEvent
    {
        public Ignition Ignition { get; }

        public IgnitionChangedEvent(Ignition ignition)
        {
            Ignition = ignition;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}