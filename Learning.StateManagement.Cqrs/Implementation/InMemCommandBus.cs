using System;
using System.Collections.Generic;
using Autofac;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Implementation
{
    public class InMemCommandBus : ICommandBus
    {
        private readonly ILifetimeScope _scope;

        //private Dictionary<ICommand, Func<Type, Func<ICommand>>> handlers = new Dictionary<ICommand, Func<Type, Func<ICommand>>>();
        private Dictionary<Type, Action<ICommand>> _handlers = new Dictionary<Type, Action<ICommand>>();

        public InMemCommandBus(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public void Dispatch<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            //_handlers[cmd](cmd);
            Action<ICommand> handler;
            if (_handlers.TryGetValue(typeof(TCommand), out handler))
            {
                handler(cmd);
            }
            else
            {
                throw new InvalidOperationException("No Handler registered");
            }
        }

        public void RegisterHandler<THandler, TCommand>(Func<THandler, Action<TCommand>> handleSelector) where TCommand : ICommand
        {
            if (_handlers.TryGetValue(typeof(TCommand), out Action<ICommand> handle))
                throw new AccessViolationException($"Command: '{typeof(TCommand)}', is already attached to Handler: '{handle.Method.GetParameters()[0].GetType()}'");

            var handler = _scope.Resolve<THandler>();
            var action = handleSelector(handler);
            _handlers.Add(typeof(TCommand), x => action((TCommand) x));
        }
    }
}