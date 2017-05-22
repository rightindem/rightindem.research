using System;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IEventBus
    {
        void Subscribe<TSubscriber, TEvent>(Func<TSubscriber, Action<TEvent>> when) where TEvent : IEvent;
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}