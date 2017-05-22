using System;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public class AggregateState
    {
        public Guid Id { get; protected set; }
        public void Mutate(IEvent @event)
        {
            ((dynamic)this).When((dynamic)@event);
        }
    }
}