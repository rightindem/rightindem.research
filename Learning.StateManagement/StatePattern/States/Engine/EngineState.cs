namespace Learning.StateManagement.StatePattern.States.Engine
{
    public abstract class EngineState
    {
        public virtual string Name { get; }
        public abstract void StartEngine(Car car);
        public abstract void StopEngine(Car car);
    }
}