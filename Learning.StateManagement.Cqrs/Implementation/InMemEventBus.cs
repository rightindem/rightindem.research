using System;
using System.Collections.Generic;
using Autofac;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Implementation
{
    public class InMemEventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Action<IEvent>>> _mappings = new Dictionary<Type, List<Action<IEvent>>>();
        private readonly ILifetimeScope _scope;

        public InMemEventBus(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Subscribe<TSubscriber,TEvent>(Func<TSubscriber, Action<TEvent>> when) where TEvent: IEvent
        {
            List<Action<IEvent>> subscribers;
            if (!_mappings.TryGetValue(typeof(TEvent), out subscribers))
            {
                subscribers = new List<Action<IEvent>>();
                _mappings.Add(typeof(TEvent), subscribers);
            }

            TSubscriber consumer = _scope.Resolve<TSubscriber>();

            Action<TEvent> action = when(consumer);

            if (!subscribers.Contains(x => action((TEvent)x)))
            {
                subscribers.Add(x => action((TEvent)x));
            }
            else
            {
                throw new AccessViolationException($"Subscriber: '{action.Method.GetParameters()[0].GetType()}', is already attached to Event: '{typeof(TEvent)}'");
            }
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (_mappings.TryGetValue(@event.GetType(), out List<Action<IEvent>> subscribers))
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