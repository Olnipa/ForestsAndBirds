public class UIStateMachine : StateMachine
{
    private static IState _currentState;

    public override void Initialize<UIPanelsState>(UIPanelsState initialState)
    {
        _currentState = initialState;
        _currentState.Enter();
    }

    public override void ChangeState<UIPanelsState>(UIPanelsState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}