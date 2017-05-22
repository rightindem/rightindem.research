using System;
using Learning.StateManagement.StatePattern.States.Engine;

namespace Learning.StateManagement.StatePattern.States.Ignition
{
    public class CarIgnitionStoppedState : IgnitionState
    {
        public override string Name => "Ignition Stopped";

        public CarIgnitionStoppedState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Car ingition stopped ===");
            Console.ResetColor();
        }

        public override void StartIgnition(Car car)
        {
            if (car.EngineState is CarEngineStartedState)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot start ignition if engine is ON");
                Console.ResetColor();
                return;
            }
            car.IgnitionState = new CarIgnitionStartedState();
        }

        public override void StopIgnition(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Igniton already stopped");
            Console.ResetColor();
        }
    }

   
}