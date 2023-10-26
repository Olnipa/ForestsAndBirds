using System;

public class FightPanelInitializer : IDisposable
{
    private StartMissionInitializer _startMissionInitializer;
    private MissionData _tempMissionData;
    private HeroSelector _heroSelector;

    public FightPanel FightPanel { get; private set; }

    public event Action<MissionData> MissionCompleted;

    public FightPanelInitializer(FightPanel fightPanel, StartMissionInitializer missionPanelsController, HeroSelector heroSelector)
    {
        FightPanel = fightPanel;
        _heroSelector = heroSelector;
        _startMissionInitializer = missionPanelsController;
        _startMissionInitializer.StartButtonClickConfirmed += OnConfirmedStartButtonClick;
    }

    public void Dispose()
    {
        _startMissionInitializer.StartButtonClickConfirmed -= OnConfirmedStartButtonClick;
    }

    private void OnConfirmedStartButtonClick(MissionData missionData)
    {
        _tempMissionData = missionData;
        FightPanel.Initialize(_tempMissionData);
        _tempMissionData.SetNewState(MissionState.TemporaryBlocked);

        FightPanel.FinishButtonCllicked += OnFinishButtonClick;
        FightPanel.Disabled += OnFightPanelDisable;
    }

    private void OnFinishButtonClick()
    {
        MissionCompleted?.Invoke(_tempMissionData);
        _tempMissionData?.SetNewState(MissionState.Completed);
        _heroSelector.SelectedHero.ToUnSelect();
    }

    private void OnFightPanelDisable()
    {
        FightPanel.FinishButtonCllicked -= OnFinishButtonClick;
        FightPanel.Disabled -= OnFightPanelDisable;
    }
}
