public abstract class StateMachine
{
    public abstract void ChangeState<TState>(TState initialState) where TState : IState;
}