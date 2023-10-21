public abstract class StateMachine
{
    //public abstract IState CurrentState { get; protected set; }

    public abstract void Initialize<TState>(TState initialState) where TState : IState;

    public abstract void ChangeState<TState>(TState initialState) where TState : IState;
}