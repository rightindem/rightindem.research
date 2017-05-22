namespace Learning.StateManagement.Cqrs.Domain
{
    public enum Engine
    {
        Stoppped = 0,
        Started = 1
    }

    public enum Ignition
    {
        Off = 0,
        On = 1
    }

    public enum Locking
    {
        Locked = 0,
        Unlocked = 1,
    }
}