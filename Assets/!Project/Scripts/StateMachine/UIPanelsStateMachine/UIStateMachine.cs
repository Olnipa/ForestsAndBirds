public class UIStateMachine : StateMachine
{
    private static IState CurrentState;

    public override void Initialize<UIPanelsState>(UIPanelsState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public override void ChangeState<UIPanelsState>(UIPanelsState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}