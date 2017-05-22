namespace Learning.StateManagement.Cqrs.Infrastructure
{
    public interface IHandle<in T> where T: ICommand
    {
        void Handle(T cmd);
    }
}