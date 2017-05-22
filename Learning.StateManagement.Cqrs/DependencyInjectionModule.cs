using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Learning.StateManagement.Cqrs.Implementation;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs
{
    public class DependencyInjectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemEventBus>().As<IEventBus>().SingleInstance();
            builder.RegisterType<InMemCommandBus>().As<ICommandBus>().SingleInstance();
            builder.RegisterType<InMemKeyValueStore>().As<IKeyValueStore>().SingleInstance();

            var handlerTypes = GetHandlerTypes<ICommand>().ToList();
            handlerTypes.ForEach(x => builder.RegisterType(x));

            var projectionTypes = GetProjectionTypes<IEvent>().ToList();
            projectionTypes.ForEach(x => builder.RegisterType(x));

            base.Load(builder);
        }

        private IEnumerable<Type> GetHandlerTypes<TCommand>() where TCommand : ICommand
        {
            var handlers = typeof(IHandle<>).Assembly.GetExportedTypes();
            var inheritingFromIHandle = handlers.Where(x => x.GetInterfaces().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IHandle<>)));
            var usingICommand = inheritingFromIHandle.Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(arg => typeof(TCommand).IsAssignableFrom(arg)))).ToList();
            return usingICommand;
        }

        private IEnumerable<Type> GetProjectionTypes<TEvent>() where TEvent : IEvent
        {
            var projections = typeof(IEventConsumer<>).Assembly.GetExportedTypes();
            var inheritingFromIEventConsumer = projections.Where(x => x.GetInterfaces().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventConsumer<>)));
            var notAggregateStates = inheritingFromIEventConsumer.Where(x => !x.IsSubclassOf(typeof(AggregateState)));
            var usingIEvent = notAggregateStates.Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(arg => typeof(TEvent).IsAssignableFrom(arg)))).ToList();
            return usingIEvent;
        }
    }
}
