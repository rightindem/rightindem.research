using System;
using Learning.StateManagement.StatePattern.States.Engine;
using Learning.StateManagement.StatePattern.States.Ignition;

namespace Learning.StateManagement.StatePattern.States.CentralLocking
{
    public class CarUnlockedState : LockingState
    {
        public override string Name => "Unlocked";

        public CarUnlockedState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Car Unlocked ===");
            Console.ResetColor();
        }

        public override void Lock(Car car)
        {
            car.LockingState = new CarLockedState();
        }

        public override void Unlock(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Car is already unlocked");
            Console.ResetColor();
            
        }
    }
}