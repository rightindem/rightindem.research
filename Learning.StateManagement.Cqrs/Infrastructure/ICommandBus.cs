using System;

namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface ICommandBus
    {
        void Dispatch<TCommand>(TCommand cmd) where TCommand : ICommand;
        void RegisterHandler<THandler, TCommand>(Func<THandler, Action<TCommand>> handleSelector) where TCommand: ICommand;
    }
}