public class DoubleMissionButtonModel : MissionButtonModel
{
    private MissionPanel _rightMissionPanel;
    private MissionData _secondMissionData;

    private const char DecimalSeparator = '.';

    public DoubleMissionButtonModel(MissionData firstMission, MissionData secondMission, MissionPanel leftMissionPanelPrefab, 
        MissionPanel rightMissionPanel) : base (firstMission, leftMissionPanelPrefab)
    {
        _secondMissionData = secondMission;
        _rightMissionPanel = rightMissionPanel;

        _secondMissionData.StateUpdated += OnMissionDataUpdated;
    }

    public override void OnMissionInfoButtonClick()
    {
        base.OnMissionInfoButtonClick();
        _rightMissionPanel.Initialize(_secondMissionData);
    }

    public override MissionState GetState()
    {
        if (FirstMissionData.State == MissionState.Completed || _secondMissionData.State == MissionState.Completed)
            return MissionState.Completed;
        else if (FirstMissionData.State == MissionState.TemporaryBlocked || _secondMissionData.State == MissionState.TemporaryBlocked)
            return MissionState.TemporaryBlocked;
        else if (FirstMissionData.State == MissionState.Active || _secondMissionData.State == MissionState.Active)
            return MissionState.Active;
        else
            return MissionState.Blocked;
    }

    public override string GetID()
    {
        return FirstMissionData.ID.Split(DecimalSeparator)[0];
    }

    public override void Dispose()
    {
        base.Dispose();
        _secondMissionData.StateUpdated -= OnMissionDataUpdated;
    }
}