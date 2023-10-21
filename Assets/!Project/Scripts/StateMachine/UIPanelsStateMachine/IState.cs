public interface IState
{
    public abstract void Enter();

    public virtual void Exit() { }
}