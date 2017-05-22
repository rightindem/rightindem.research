using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Learning.StateManagement.Cqrs.Domain;
using Learning.StateManagement.Cqrs.Domain.Commands;
using Learning.StateManagement.Cqrs.Domain.Events;
using Learning.StateManagement.Cqrs.Infrastructure;
using Learning.StateManagement.Cqrs.Projections;

namespace Learning.StateManagement.Cqrs
{
    class Program
    {
        

        static void Main(string[] args)
        {
            var container = SetupIoC();
            var commandBus = container.Resolve<ICommandBus>();
            var eventBus = container.Resolve<IEventBus>();

            RegisterCommandHandlers(commandBus);
            RegisterEventHandlers(eventBus);

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
            Console.WriteLine("7. Create Radom car");
            Console.WriteLine("8. Report");

            int action;
            
            //var car = new CarAggregate();



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
                    case 7:
                        commandBus.Dispatch(new CreateCarCommand() { Id = Guid.NewGuid(), AggregateId = Guid.NewGuid(), Model = Guid.NewGuid().ToString().Substring(0, 4)});
                        break;
                    case 8:
                        var keyValueStore = container.Resolve<IKeyValueStore>();
                        var carsCreatedCount = keyValueStore.Get<int>(CarCounterProjection.Key);
                        Console.WriteLine($"Total Cars Created: {carsCreatedCount}");
                        break;
                }
            }
        }

        static void RegisterEventHandlers(IEventBus eventBus)
        {
            eventBus.Subscribe<CarCounterProjection, CarCreatedEvent>(x => x.When);
        }

        static void RegisterCommandHandlers(ICommandBus commandBus)
        {
            commandBus.RegisterHandler<CarHandler, CreateCarCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, LockCarCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, UnlockCarCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StartEngineCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StopEngineCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StartIgnitionCommand>(x => x.Handle);
            commandBus.RegisterHandler<CarHandler, StopIgnitionCommand>(x => x.Handle);
        }

        static IContainer SetupIoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            return builder.Build();
        }
    }
}

    
