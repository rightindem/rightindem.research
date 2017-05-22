namespace Learning.StateManagement.StatePattern.States.Ignition
{
    public abstract class IgnitionState
    {
        public virtual string Name { get; }
        public abstract void StartIgnition(Car car);
        public abstract void StopIgnition(Car car);
    }
}