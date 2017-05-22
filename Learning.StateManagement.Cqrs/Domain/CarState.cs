using System;
using Learning.StateManagement.Cqrs.Domain.Events;
using Learning.StateManagement.Cqrs.Infrastructure;
using Newtonsoft.Json;

namespace Learning.StateManagement.Cqrs.Domain
{
    public class CarState: AggregateState,
        IEventConsumer<EngineChangedEvent>,
        IEventConsumer<IgnitionChangedEvent>,
        IEventConsumer<LockingChangedEvent>,
        IEventConsumer<EngineStartFailedEvent>,
        IEventConsumer<IgnitionStartFailedEvent>,
        IEventConsumer<LockFailedEvent>,
        IEventConsumer<CarCreatedEvent>,
        IEventConsumer<CarCreationFailedEvent>
    {
        public string Model { get; private set; }
        public bool IsCreated { get; private set; }
        public Ignition Ignition { get; private set; }
        public Locking Locking { get; private set; }
        public Engine Engine { get; private set; }

        public void When(CarCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Model = @event.Model;
            IsCreated = true;
            LogSuccess($"Car {Model} with id: {Id} has been created");
        }

        public void When(CarCreationFailedEvent @event)
        {
            LogWarning(@event.Message);
        }

        public void When(EngineChangedEvent @event)
        {
            Engine = @event.Engine;
            LogSuccess($"Engine changed to: {Engine}");
        }

        public void When(IgnitionChangedEvent @event)
        {
            Ignition = @event.Ignition;
            LogSuccess($"Ignition changed to: {Ignition}");
        }

        public void When(LockingChangedEvent @event)
        {
            Locking = @event.Locking;
            LogSuccess($"Locking changed to: {Locking}");
        }

        public void When(EngineStartFailedEvent @event)
        {
            LogWarning(@event.Message);
        }

        public void When(EngineStopFailedEvent @event)
        {
            LogWarning(@event.Message);
        }

        public void When(IgnitionStartFailedEvent @event)
        {
            LogWarning(@event.Message);
        }

        public void When(IgnitionStopFailedEvent @event)
        {
            LogWarning(@event.Message);
        }

        public void When(LockFailedEvent @event)
        {
            LogWarning(@event.Message);
        }

        public void When(UnlockFailedEvent @event)
        {
            LogWarning(@event.Message);
        }


       
        public override string ToString()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }

        private void LogSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine(this.ToString());
        }

        private void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine(this.ToString());
        }
    }

    //public static class EnumExtensions
    //{
    //    public static void ToEnum(this enum val) 
    //    {
    //        return (T)(object)value;
    //    }

    //    public static T ToEnum<T>(this int value) where T : struct
    //    {
    //        return (T)(object)value;
    //    }

    //    public static string ToEnumName<T>(this int value) where T : struct
    //    {
    //        return ((T)(object)value).ToString();
    //    }
    //}
}