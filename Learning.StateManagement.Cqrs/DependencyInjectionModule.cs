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

            var handlerTypes = GetHandlerTypes<ICommand>();

            foreach (var handlerType in handlerTypes)
            {

                builder.RegisterType(handlerType);
            }


            
            base.Load(builder);
        }

        private IEnumerable<Type> GetCommands(Type type)
        {
            var commands = type.GetInterfaces().SelectMany(i => i.GetGenericArguments());
            return commands;
        }

        private IEnumerable<Type> GetHandlerTypes<TCommand>() where TCommand : ICommand
        {
            var handlers = typeof(IHandle<>).Assembly.GetExportedTypes();
            var inheritingFromIHandle = handlers.Where(x => x.GetInterfaces().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IHandle<>)));
            var usingICommand = inheritingFromIHandle.Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(arg => typeof(TCommand).IsAssignableFrom(arg)))).ToList();

   

            var usingICommand2 = 
                inheritingFromIHandle
                .Where(h => h.GetInterfaces()
                    .Any(ii => ii.GetGenericArguments()
                        .Any(arg => typeof(TCommand).IsAssignableFrom(arg))
                     )
                ).ToList();
            return usingICommand;
        }
    }
}
