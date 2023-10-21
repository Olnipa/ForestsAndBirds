public class MissionStateMachine : StateMachine
{
    private IState CurrentState;

    public override void Initialize<MissionButtonState>(MissionButtonState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter();
    }

    public override void ChangeState<MissionButtonState>(MissionButtonState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
