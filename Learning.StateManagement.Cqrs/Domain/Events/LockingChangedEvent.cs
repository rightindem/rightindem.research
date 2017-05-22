using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class LockingChangedEvent: IEvent
    {
        public Locking Locking { get; }

        public LockingChangedEvent(Locking locking)
        {
            Locking = locking;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}