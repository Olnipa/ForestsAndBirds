public class UIStateMachine : StateMachine
{
    private static IState _currentState;

    public override void ChangeState<UIPanelsState>(UIPanelsState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}