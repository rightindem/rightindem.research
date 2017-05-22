using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class CarCreationFailedEvent : IEvent
    {
        public CarCreationFailedEvent(Guid carId, string message, Guid causationId)
        {
            AggregateId = carId;
            Message = message;
            CausationId = causationId;
        }
        public string Message { get; }
        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}