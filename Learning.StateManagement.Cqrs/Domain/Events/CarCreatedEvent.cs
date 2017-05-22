using System;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain.Events
{
    public class CarCreatedEvent : IEvent
    {
        public CarCreatedEvent(Guid carId, Guid causationId, string model)
        {
            Model = model;
            AggregateId = carId;
            CausationId = causationId;
        }
        public string Model { get; }
        public Guid CausationId { get; }
        public Guid AggregateId { get; }
    }
}