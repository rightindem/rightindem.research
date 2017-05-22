using System;
using Learning.StateManagement.Cqrs.Domain.Events;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Projections
{
    public class CarCounterProjection: IEventConsumer<CarCreatedEvent>
    {
        public static string Key = nameof(CarCounterProjection);

        private readonly IKeyValueStore _store;

        public CarCounterProjection(IKeyValueStore store)
        {
            _store = store;
        }
        public void When(CarCreatedEvent @event)
        {
            var counter = _store.Get<int>(Key);
            counter++;
            _store.Add(Key, counter);
        }
    }
}