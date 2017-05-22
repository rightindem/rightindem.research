namespace Learning.StateManagement.StatePattern.States.CentralLocking
{
    public abstract class LockingState
    {
        public virtual string Name { get; }
        public abstract void Lock(Car car);
        public abstract void Unlock(Car car);
    }
}