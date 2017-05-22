using System;
using System.Linq;
using System.Linq.Expressions;
using Learning.StateManagement.Cqrs.Domain.Commands;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain
{
    public class CarHandler: 
        IHandle<CreateCarCommand>,
        IHandle<LockCarCommand>,
        IHandle<UnlockCarCommand>,
        IHandle<StartEngineCommand>,
        IHandle<StopEngineCommand>,
        IHandle<StartIgnitionCommand>,
        IHandle<StopIgnitionCommand>

    {
        private readonly IEventBus _eventBus;
        private readonly IKeyValueStore _store;

        public CarHandler(IEventBus eventBus, IKeyValueStore store)
        {
            _eventBus = eventBus;
            _store = store;
        }

        public void Handle(CreateCarCommand cmd)
        {
            Create(cmd, car => car.Create(cmd));
        }

        public void Handle(LockCarCommand cmd)
        {
            Perform(cmd, car => car.Lock(cmd));

            /* 
             * The long way of doing Perform(cmd, car => car.Lock(cmd)) 
             *
            
             var car = _store.Get<CarAggregate>(cmd.AggregateId);
             car.Lock(cmd);
          
             car.PendingEvents.ToList().ForEach(_eventBus.Publish);
             _store.Add(car.Id, car);
             */

        }

        public void Handle(StartEngineCommand cmd)
        {
            Perform(cmd, car => car.StartEngine(cmd));
        }

        public void Handle(UnlockCarCommand cmd)
        {
            Perform(cmd, car => car.Unlock(cmd));
        }

        public void Handle(StopEngineCommand cmd)
        {
            Perform(cmd, car => car.StopEngine(cmd));
        }

        public void Handle(StartIgnitionCommand cmd)
        {
            Perform(cmd, car => car.StartIgnition(cmd));
        }

        public void Handle(StopIgnitionCommand cmd)
        {
            Perform(cmd, car => car.StopIgnition(cmd));
        }

        
        private void Perform<T, TState>(ICommand cmd, Action<T> execute) where T : Aggregate<TState> where TState : AggregateState
        {
            var aggregateState = _store.Get<TState>(cmd.AggregateId);
            if (aggregateState == null)
                throw new Exception($"Could not find aggregate ({nameof(T)}): {cmd.AggregateId}");
            var aggregate = (T)Activator.CreateInstance(typeof(T), aggregateState);

            execute(aggregate);
            _store.Add<TState>(aggregate.Id, aggregate.State);
            aggregate.PendingEvents.ToList().ForEach(_eventBus.Publish);
        }

        private void Perform(ICommand cmd, Action<CarAggregate> cmdSelector)
        {
            Perform<CarAggregate, CarState>(cmd, cmdSelector);
        }

        private void Create<T, TState>(ICommand cmd, Action<T> execute) where T : Aggregate<TState> where TState : AggregateState
        {
            var aggregateState = _store.Get<TState>(cmd.AggregateId);
            if (aggregateState != null)
                throw new Exception($"Aggregate with id: {cmd.AggregateId} already exists");
            var aggregate = Activator.CreateInstance<T>();
            execute(aggregate);
            _store.Add<TState>(aggregate.Id, aggregate.State);
            aggregate.PendingEvents.ToList().ForEach(_eventBus.Publish);
        }

        private void Create(ICommand cmd, Action<CarAggregate> cmdSelector)
        {
            Create<CarAggregate, CarState>(cmd, cmdSelector);
        }
    }
}