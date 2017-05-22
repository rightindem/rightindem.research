using System;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IEvent
    {
        Guid CausationId { get; }
        Guid AggregateId { get; }
       
    }

    public class Event : IEvent
    {
        public Guid Id { get; }
        public Guid CausationId { get; }
        public Guid AggregateId { get; }

        public Event(Guid id, Guid aggregateId, Guid causationId)
        {
            Id = id;
            AggregateId = aggregateId;
            CausationId = causationId;
        }

        public static IEvent CreateFrom(ICommand cmd)
        {
            return new Event(Guid.NewGuid(), cmd.AggregateId, cmd.AggregateId);
        }
    }
}