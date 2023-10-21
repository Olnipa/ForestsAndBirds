public class DoubleMissionButton : MissionButton
{
    private MissionPanel _rightMissionPanelPrefab;
    private MissionData _secondMissionData;

    public DoubleMissionButton(MissionData firstMission, MissionData secondMission, MissionPanel leftMissionPanelPrefab, 
        MissionPanel rightMissionPanelPrefab) : base (firstMission, leftMissionPanelPrefab)
    {
        _secondMissionData = secondMission;
        _rightMissionPanelPrefab = rightMissionPanelPrefab;
    }

    private void OnSecondMissionDataUpdated()
    {
        _buttonView.SetNewState(_secondMissionData.State);
    }

    protected override void OnMissionInfoButtonClick()
    {
        base.OnMissionInfoButtonClick();
        _rightMissionPanelPrefab.Initialize(_secondMissionData);
        _rightMissionPanelPrefab.gameObject.SetActive(true);
    }

    protected override MissionState GetState()
    {
        if (_firstMissionData.State == MissionState.Completed || _secondMissionData.State == MissionState.Completed)
            return MissionState.Completed;
        else if (_firstMissionData.State == MissionState.TemporaryBlocked || _secondMissionData.State == MissionState.TemporaryBlocked)
            return MissionState.TemporaryBlocked;
        else if (_firstMissionData.State == MissionState.Active || _secondMissionData.State == MissionState.Active)
            return MissionState.Active;
        else
            return MissionState.Blocked;
    }

    protected override string GetID()
    {
        string[] ID = _firstMissionData.ID.Split('.');

        return ID[0];
    }

    protected override void AddListenerToMissionDataUpdate()
    {
        _firstMissionData.StateUpdated += OnFirstMissionDataUpdated;
        _secondMissionData.StateUpdated += OnSecondMissionDataUpdated;
    }
}