using Learning.StateManagement.Cqrs.Domain.Commands;
using Learning.StateManagement.Cqrs.Domain.Events;
using Learning.StateManagement.Cqrs.Infrastructure;

namespace Learning.StateManagement.Cqrs.Domain
{
    public class CarAggregate: Aggregate<CarState>
    {
        public CarAggregate()
        {
        }
        public CarAggregate(CarState state): base(state)
        {
        }
        public void Create(CreateCarCommand cmd)
        {
            if (this.State.IsCreated)
                Apply(new CarCreationFailedEvent(cmd.AggregateId, "Already created", cmd.Id));
            Apply(new CarCreatedEvent(cmd.AggregateId, cmd.Id, cmd.Model));
        }

        public void StartEngine(StartEngineCommand cmd)
        {
            if (State.Engine == Engine.Started)
            {
                Apply(new EngineStartFailedEvent("Engine already started"));
                return;
            }

            if (State.Ignition == Ignition.Off)
            {
                Apply(new EngineStartFailedEvent("Turn ON ignition first"));
                return;
            }

            Apply(new EngineChangedEvent(Engine.Started));
        }

        public void StopEngine(StopEngineCommand cmd)
        {
            if (State.Engine == Engine.Stoppped)
            {
                Apply(new EngineStopFailedEvent("Engine already stopped"));
                return;
            }

            if (State.Ignition == Ignition.Off)
            {
                Apply(new EngineStopFailedEvent("Engine must be already stopped because ignition is OFF"));
                return;
            }
            Apply(new EngineChangedEvent(Engine.Stoppped));
        }

        public void StartIgnition(StartIgnitionCommand cmd)
        {
            if (State.Engine == Engine.Started)
            {
                Apply(new IgnitionStartFailedEvent("Ignition must be already started because engine is ON"));
                return;
            }

            if (State.Ignition == Ignition.On)
            {
                Apply(new IgnitionStartFailedEvent("Ignition already is ON"));
                return;
            }
            Apply(new IgnitionChangedEvent(Ignition.On));
        }

        public void StopIgnition(StopIgnitionCommand cmd)
        {
            if (State.Ignition == Ignition.Off)
            {
                Apply(new IgnitionStopFailedEvent("Ignition is already OFF"));
                return;
            }

            if (State.Engine == Engine.Started)
            {
                Apply(new IgnitionStopFailedEvent("Stop engine first"));
                return;
            }
            Apply(new IgnitionChangedEvent(Ignition.Off));
        }

        public void Lock(LockCarCommand cmd)
        {
            if (State.Locking == Locking.Locked)
            {
                Apply(new LockFailedEvent("Car already locked"));
                return;
            }
            Apply(new LockingChangedEvent(Locking.Locked));
        }

        public void Unlock(UnlockCarCommand cmd)
        {
            if (State.Locking == Locking.Unlocked)
            {
                Apply(new UnlockFailedEvent("Car already Unlocked"));
                return;
            }
            Apply(new LockingChangedEvent(Locking.Unlocked));
        }
    }
}