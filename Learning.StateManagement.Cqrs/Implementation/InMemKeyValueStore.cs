using System.Collections.Concurrent;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Implementation
{
    public class InMemKeyValueStore : IKeyValueStore
    {
        private readonly ConcurrentDictionary<object, object> store = new ConcurrentDictionary<object, object>();

        public void Add<T>(object key, T value)
        {
            store.AddOrUpdate(key, value, (o, o1) => value);
        }

        public T Get<T>(object key)
        {
            object result;
            store.TryGetValue(key, out result);
            return (T) result;
        }
    }
}