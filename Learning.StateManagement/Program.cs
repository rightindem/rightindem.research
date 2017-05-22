using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learning.StateManagement.StatePattern;
using Learning.StateManagement.StatePattern.States;
using Learning.StateManagement.StatePattern.States.CentralLocking;
using Learning.StateManagement.StatePattern.States.Engine;
using Learning.StateManagement.StatePattern.States.Ignition;

namespace Learning.StateManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var locking = new CarLockedState();
            var ignition = new CarIgnitionStoppedState();
            var engine = new CarEngineStoppedState();
            var car = new Car(locking, ignition, engine);
            
            
            Console.WriteLine("Please selected actions (1 - 6)");
            Console.WriteLine("1. Start Ignition");
            Console.WriteLine("2. Stop Ignition");
            Console.WriteLine("3. Start Engine");
            Console.WriteLine("4. Stop Engine");
            Console.WriteLine("5. Lock");
            Console.WriteLine("6. Unlock");

            int action;

            while (Int32.TryParse(Console.ReadLine(), out action))
            {
                switch (action)
                {
                    case 1:
                        car.StartIgnition();
                        continue;
                    case 2:
                        car.StopIgnition();
                        break;
                    case 3:
                        car.StartEngine();
                        break;
                    case 4:
                        car.StopEngine();
                        break;
                    case 5:
                        car.Lock();
                        break;
                    case 6:
                        car.Unlock();
                        break;
                }
            }
            
        }
    }
}
