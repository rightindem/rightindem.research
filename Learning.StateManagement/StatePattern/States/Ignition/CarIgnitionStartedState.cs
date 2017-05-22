using System;
using Learning.StateManagement.StatePattern.States.Engine;

namespace Learning.StateManagement.StatePattern.States.Ignition
{
    public class CarIgnitionStartedState : IgnitionState
    {
        public override string Name => "Ignition started";

        public CarIgnitionStartedState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Car ingition started ===");
            Console.ResetColor();
        }

        public override void StartIgnition(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ignition is already ON");
            Console.ResetColor();
        }      

        public override void StopIgnition(Car car)
        {
            if (car.EngineState is CarEngineStartedState)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please stop engine first");
                Console.ResetColor();
                return;
            }
                
            car.IgnitionState = new CarIgnitionStoppedState();
        }
    }
}