using System;
using Learning.StateManagement.StatePattern.States.Ignition;

namespace Learning.StateManagement.StatePattern.States.Engine
{
    public class CarEngineStoppedState : EngineState
    {
        public override string Name => "Engine stopped";

        public CarEngineStoppedState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Car engine stopped ===");
            Console.ResetColor();
        }
        
        public override void StartEngine(Car car)
        {
            if (EnsureIgnitionStopped(car))
            {
                car.EngineState = new CarEngineStartedState();
            }
        }

        public override void StopEngine(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Engine already stopped");
            Console.ResetColor();
            
        }

        private bool EnsureIgnitionStopped(Car car)
        {
            if (car.IgnitionState is CarIgnitionStoppedState)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Start ignition first");
                Console.ResetColor();
                return false;
            }
            return true;
        }
    }
}