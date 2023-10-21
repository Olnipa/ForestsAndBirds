public class DoubleMissionInfoUIState : UIPanelsState
{
    private MissionPanel _firstMissionPanel;
    private MissionPanel _SecondMissionPanel;

    public DoubleMissionInfoUIState(MissionPanel leftMissionPanel, MissionPanel rightMissionPanel)
    {
        _firstMissionPanel = leftMissionPanel;
        _SecondMissionPanel = rightMissionPanel;
    } 

    public override void Enter()
    {
        _firstMissionPanel.EnablePanel();
    }

    public override void Exit()
    {
        _firstMissionPanel.DisablePanel();
        _SecondMissionPanel.DisablePanel();
    }
}