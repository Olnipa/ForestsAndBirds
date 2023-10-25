public class MissionStateMachine : StateMachine
{
    private IState _currentState;

    public override void ChangeState<MissionButtonState>(MissionButtonState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
