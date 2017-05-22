namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IEventConsumer<in T>
    {
        void When(T @event);
    }
}