using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Learning.StateManagement.Cqrs.Domain;
using Learning.StateManagement.Cqrs.Domain.Commands;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs
{


    public class Person
    {
        public void Greet<T>(T obj)
        {
            Console.WriteLine($"Greeting {obj}");
        }

        public void Ola<T, TC>(Func<T, Action<TC>> obj)
        {
            Console.WriteLine($"Ola {obj}");
        }
    }

    class Program
    {
        private static IContainer Container;


        static void Main(string[] args)
        {
            Container = SetupIoC();

            var commandBus = Container.Resolve<ICommandBus>();

            commandBus.RegisterHandler<CarHandler, CreateCarCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, LockCarCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, UnlockCarCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StartEngineCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StopEngineCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StartIgnitionCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StopIgnitionCommand>(x => x.Handle);

            var carId = Guid.NewGuid();

            commandBus.Dispatch(new CreateCarCommand()
            {
                Id = Guid.NewGuid(),
                AggregateId = carId,
                Model = "BMW X5"
            });

            Console.WriteLine("Please selected actions (1 - 6)");
            Console.WriteLine("1. Start Ignition");
            Console.WriteLine("2. Stop Ignition");
            Console.WriteLine("3. Start Engine");
            Console.WriteLine("4. Stop Engine");
            Console.WriteLine("5. Lock");
            Console.WriteLine("6. Unlock");

            int action;

            while (Int32.TryParse(Console.ReadLine(), out action))
            {
                switch (action)
                {
                    case 1:
                        commandBus.Dispatch(new StartIgnitionCommand() { AggregateId = carId });

                        break;
                    case 2:
                        commandBus.Dispatch(new StopIgnitionCommand() { AggregateId = carId });

                        break;
                    case 3:
                        commandBus.Dispatch(new StartEngineCommand() { AggregateId = carId });

                        break;
                    case 4:
                        commandBus.Dispatch(new StopEngineCommand() { AggregateId = carId });

                        break;
                    case 5:
                        commandBus.Dispatch(new LockCarCommand() { AggregateId = carId });

                        break;
                    case 6:
                        commandBus.Dispatch(new UnlockCarCommand() { AggregateId = carId });
                        
                        break;
                }
                //car.PrintEvents();
            }
        }

        static IContainer SetupIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }

    public interface IKeyValueStore
    {
        void Add<T>(object key, T value);
        T Get<T>(object key);
    }

    public class KeyValueStore : IKeyValueStore
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


    public static class InvocationHelper
    {
        public static MethodInfo GetMethod<T>(Expression<Func<T, object>> expression)
        {
            MethodCallExpression methodCall = (MethodCallExpression) expression.Body;
            return methodCall.Method;
        }


        public static MethodInfo GetMethod(
            Expression<Func<ICommandBus, Action<Func<Type, Action<ICommand>>>>> expression)
        {
            MethodCallExpression methodCall = (MethodCallExpression) expression.Body;
            return methodCall.Method;
        }

        public static object InvokeGenericMethodWithDynamicTypeArguments<T>(T target,
            Expression<Func<ICommandBus, Action<Func<Type, Action<ICommand>>>>> expression, 
            object[] methodArguments,
            params Type[] typeArguments)
        {


            var methodInfo = GetMethod(expression);
            if (methodInfo.GetGenericArguments().Length != typeArguments.Length)
                throw new ArgumentException(
                    string.Format(
                        "The method '{0}' has {1} type argument(s) but {2} type argument(s) were passed. The amounts must be equal.",
                        methodInfo.Name,
                        methodInfo.GetGenericArguments().Length,
                        typeArguments.Length));

            return methodInfo
                .GetGenericMethodDefinition()
                .MakeGenericMethod(typeArguments)
                .Invoke(target, methodArguments);
        }
    }
}

    
