using System;

namespace Learning.StateManagement.StatePattern.States.CentralLocking
{
    public class CarLockedState : LockingState
    {
        public override string Name => "Locked";

        public CarLockedState()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Car Locked ===");
            Console.ResetColor();
        }

        public override void Lock(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Car already locked");
            Console.ResetColor();
              
        }

        public override void Unlock(Car car)
        {
            car.LockingState = new CarUnlockedState();
        }
    }
}