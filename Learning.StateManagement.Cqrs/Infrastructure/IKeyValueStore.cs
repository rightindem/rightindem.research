namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IKeyValueStore
    {
        void Add<T>(object key, T value);
        T Get<T>(object key);
    }
}