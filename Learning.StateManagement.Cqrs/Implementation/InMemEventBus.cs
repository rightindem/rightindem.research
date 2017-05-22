using System;
using System.Collections.Generic;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Implementation
{
    public class InMemEventBus : IEventBus
    {
        private Dictionary<Type, List<Action<IEvent>>> _mappings = new Dictionary<Type, List<Action<IEvent>>>();

        public void Subscribe<TEvent, TSubscriber>(Action<TEvent> action) where TEvent: IEvent
        {
            if (!_mappings.TryGetValue(typeof(TEvent), out List<Action<IEvent>> subscribers))
            {
                subscribers = new List<Action<IEvent>>();
                _mappings.Add(typeof(TEvent), subscribers);
            }


            if (subscribers.Contains(x => action((TEvent) x)))
            {
                throw new AccessViolationException(
                    $"Subscriber: '{action.Method.GetParameters()[0].GetType()}', is already attached to Event: '{typeof(TEvent)}'");
            }
            subscribers.Add(x => action((TEvent)x));
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (_mappings.TryGetValue(typeof(TEvent), out List<Action<IEvent>> subscribers))
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber(@event);
                }
               
            }
        }
    }

    //public interface IRepository<T>
    //{
    //    T GetById(Guid aggregateId);
    //    void Save(T aggregate);
    //}

    //public class Repository<T> : IRepository<T> where T: new()
    //{

    //    public T GetById(Guid aggregateId)
    //    {
    //        var events = EventStore.GetEvents(aggregateId);
    //        var aggregate = new T();//Activator.CreateInstance<T>();
    //        aggregate.LoadFromHistory(events);
    //        return aggregate;
    //    }

    //    public void Save(T aggregate)
    //    {
    //        EventStore.Save(aggregate.State.Id, aggregate.GetUncommittedChanges());
    //    }
    //}
}