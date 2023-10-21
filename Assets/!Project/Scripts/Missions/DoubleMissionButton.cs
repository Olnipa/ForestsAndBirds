public class DoubleMissionButton : MissionButton
{
    private MissionPanel _rightMissionPanelPrefab;
    private MissionData _secondMissionData;

    private const char DecimalSeparator = '.';

    public DoubleMissionButton(MissionData firstMission, MissionData secondMission, MissionPanel leftMissionPanelPrefab, 
        MissionPanel rightMissionPanelPrefab) : base (firstMission, leftMissionPanelPrefab)
    {
        _secondMissionData = secondMission;
        _rightMissionPanelPrefab = rightMissionPanelPrefab;
    }

    private void OnSecondMissionDataUpdated()
    {
        ButtonView.SetNewState(_secondMissionData.State);
    }

    protected override void OnMissionInfoButtonClick()
    {
        base.OnMissionInfoButtonClick();
        _rightMissionPanelPrefab.Initialize(_secondMissionData);
        _rightMissionPanelPrefab.gameObject.SetActive(true);
    }

    protected override MissionState GetState()
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

    protected override string GetID()
    {
        string[] ID = FirstMissionData.ID.Split(DecimalSeparator);

        return ID[0];
    }

    protected override void AddListenerToMissionDataUpdate()
    {
        FirstMissionData.StateUpdated += OnFirstMissionDataUpdated;
        _secondMissionData.StateUpdated += OnSecondMissionDataUpdated;
    }
}