using System;
using Learning.StateManagement.StatePattern.States;
using Learning.StateManagement.StatePattern.States.CentralLocking;
using Learning.StateManagement.StatePattern.States.Engine;
using Learning.StateManagement.StatePattern.States.Ignition;

namespace Learning.StateManagement.StatePattern
{
    public class Car
    {
        public IgnitionState IgnitionState { get; set; }
        public LockingState LockingState { get; set; }
        public EngineState EngineState { get; set; }

        public Car() { }
        public Car(LockingState lockingState, IgnitionState ignitionState, EngineState engineState)
        {
            LockingState = lockingState;
            IgnitionState = ignitionState;
            EngineState = engineState;
        }

        public void StartIgnition()
        {
            IgnitionState.StartIgnition(this);
            Console.WriteLine(this);
        }
        public void StopIgnition()
        {
            IgnitionState.StopIgnition(this);
            Console.WriteLine(this);
        }
        public void StartEngine()
        {
            EngineState.StartEngine(this);
            Console.WriteLine(this);
        }
        public void StopEngine()
        {
            EngineState.StopEngine(this);
            Console.WriteLine(this);
        }
        public void Lock()
        {
            LockingState.Lock(this);
            Console.WriteLine(this);
        }
        public void Unlock()
        {
            LockingState.Unlock(this);
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"Ignition: {IgnitionState.Name} \nLocking: {LockingState.Name} \nEngine: {EngineState.Name}";
        }
    }

}