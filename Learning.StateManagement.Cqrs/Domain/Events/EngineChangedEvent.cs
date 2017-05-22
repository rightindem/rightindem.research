using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class EngineChangedEvent: IEvent
    {
        public Engine Engine { get; }

        public EngineChangedEvent(Engine engine)
        {
            Engine = engine;
        }

        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}