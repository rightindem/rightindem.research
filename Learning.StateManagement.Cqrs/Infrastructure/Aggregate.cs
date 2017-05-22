using System;
using System.Collections.Generic;
using System.Data;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public class Aggregate<T> where T: AggregateState
    {
        public Guid Id => State.Id;
        public T State { get; }
        public IList<IEvent> PendingEvents { get; }
        public Aggregate()
        {
            State = Activator.CreateInstance<T>();
            PendingEvents = new List<IEvent>();
        }

        public void Apply(IEvent @event, bool isNew = true)
        {
            if (@event.AggregateId == null)
                throw new ConstraintException("AggregateId not specified");

            if (isNew)
            {
                PendingEvents.Add(@event);
            }
            State.Mutate(@event);
        }

        public void Replay(IList<IEvent> events)
        {
            foreach (var @event in events)
            {
                this.Apply(@event, false);
            }
        }
        
        public void PrintEvents()
        {
            foreach (var @event in PendingEvents)
            {
                Console.WriteLine(@event.ToString());
            }
        }
    }
}