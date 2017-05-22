using System;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IEventBus
    {
        void Subscribe<TEvent, TSubscriber>(Action<TEvent> action) where TEvent : IEvent;
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}