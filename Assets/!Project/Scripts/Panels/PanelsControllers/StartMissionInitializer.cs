using System;
using UnityEngine.UI;

public class StartMissionInitializer : IDisposable
{
    private HeroSelector _heroSelector;

    public MissionPanel LeftMissionPanel {get; private set;}
    public MissionPanel RightMissionPanel { get; private set; }

    public event Action HeroIsNotChosen;
    public event Action<MissionData> StartButtonClickConfirmed;
    public event Action Disposed;

    public StartMissionInitializer(Button closePanelsButton, MissionPanel leftMissionPanel, 
        MissionPanel rightMissionPanel, FightPanel fightPanel, HeroSelector heroSelector)
    {
        _heroSelector = heroSelector;

        LeftMissionPanel = leftMissionPanel;
        RightMissionPanel = rightMissionPanel;

        LeftMissionPanel.Enabled += OnLeftMissionPanelEnabled;
        RightMissionPanel.Enabled += OnRightMissionPanelEnabled;
    }

    public void Dispose()
    {
        LeftMissionPanel.Enabled -= OnLeftMissionPanelEnabled;
        RightMissionPanel.Enabled -= OnRightMissionPanelEnabled;
        Disposed?.Invoke();
    }

    private void OnLeftMissionPanelEnabled()
    {
        LeftMissionPanel.StartButtonClicked += OnStartButtonClick;
        LeftMissionPanel.Disabled += OnLeftMissionPanelDisable;
    }

    private void OnRightMissionPanelEnabled()
    {
        RightMissionPanel.StartButtonClicked += OnStartButtonClick;
        RightMissionPanel.Disabled += OnRightMissionPanelDisable;
    }

    private void OnLeftMissionPanelDisable()
    {
        LeftMissionPanel.StartButtonClicked -= OnStartButtonClick;
        LeftMissionPanel.Disabled -= OnLeftMissionPanelDisable;
    }

    private void OnRightMissionPanelDisable()
    {
        RightMissionPanel.StartButtonClicked -= OnStartButtonClick;
        RightMissionPanel.Disabled -= OnRightMissionPanelDisable;
    }

    private void OnStartButtonClick(MissionData missionData)
    {
        if (_heroSelector.SelectedHero == null)
        {
            HeroIsNotChosen?.Invoke();
            return;
        }

        missionData.SetNewState(MissionState.TemporaryBlocked);
        StartButtonClickConfirmed?.Invoke(missionData);
    }
}