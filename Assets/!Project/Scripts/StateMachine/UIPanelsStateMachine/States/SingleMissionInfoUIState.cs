public class SingleMissionInfoUIState : UIPanelsState
{
    private MissionPanel _missionPanel;

    public SingleMissionInfoUIState(MissionPanel missionPanel)
    {
        _missionPanel = missionPanel;
    }

    public override void Enter()
    {
        _missionPanel.EnablePanel();
    }

    public override void Exit()
    {
        _missionPanel.DisablePanel();
    }
}
