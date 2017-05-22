using System;
using Learning.StateManagement.StatePattern.States.Ignition;

namespace Learning.StateManagement.StatePattern.States.Engine
{
    public class CarEngineStartedState : EngineState
    {
        public override string Name => "Engine Started";

        public CarEngineStartedState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Car Engine started ===");
            Console.ResetColor();
        }

        public override void StartEngine(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Engine already started");
            Console.ResetColor();
           
        }

        public override void StopEngine(Car car)
        {
            car.EngineState = new CarEngineStoppedState();
        }
        
    }
}