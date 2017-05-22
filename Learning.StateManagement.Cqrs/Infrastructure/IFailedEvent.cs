namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IFailedEvent: IEvent
    {
        string Message { get; }
    }
}