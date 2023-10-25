using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionPanelsController : IUICloser, IDisposable
{
    private readonly List<IPanelUI> _panels = new List<IPanelUI>();
    
    private HeroesController _heroesController;
    private MissionData _tempMissionData;

    public Button _closePanelsButton { get; private set; }
    public MissionPanel LeftMissionPanel {get; private set;}
    public MissionPanel RightMissionPanel { get; private set; }
    public FightPanel FightPanel { get; private set; }

    public event UnityAction HeroIsNotChosen;
    public event UnityAction StartButtonClickConfirmed;
    public event UnityAction FinishButtonClicked;
    public event UnityAction Disposed;

    public MissionPanelsController(Button closePanelsButton, MissionPanel leftMissionPanel, 
        MissionPanel rightMissionPanel, FightPanel fightPanel)
    {
        _closePanelsButton = closePanelsButton;
        LeftMissionPanel = leftMissionPanel;
        RightMissionPanel = rightMissionPanel;
        FightPanel = fightPanel;
    }

    public void Dispose()
    {
        _closePanelsButton.onClick.RemoveListener(CloseAllPanels);
        _closePanelsButton.onClick.RemoveListener(_heroesController.EnablePossibilityToChooseHero);
        LeftMissionPanel.Enabled -= OnLeftMissionPanelEnabled;
        RightMissionPanel.Enabled -= OnRightMissionPanelEnabled;
        Disposed?.Invoke();
    }

    public void CloseAllPanels()
    {
        foreach (var panel in _panels)
        {
            panel.DisablePanel();
        }
    }

    public void Initialize(HeroesController heroesController)
    {
        _heroesController = heroesController;
        _panels.AddRange(new List<IPanelUI>() { LeftMissionPanel, RightMissionPanel, FightPanel });
        CloseAllPanels();

        LeftMissionPanel.Enabled += OnLeftMissionPanelEnabled;
        RightMissionPanel.Enabled += OnRightMissionPanelEnabled;

        _closePanelsButton.onClick.AddListener(CloseAllPanels);
        _closePanelsButton.onClick.AddListener(_heroesController.EnablePossibilityToChooseHero);
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

    private void OnFightPanelDisable()
    {
        FightPanel.FinishButtonCllicked -= OnFinishButtonClick;
        FightPanel.Disabled -= OnFightPanelDisable;
    }

    private void OnStartButtonClick(MissionData missionData)
    {
        if (_heroesController.SelectedHeroModel == null)
        {
            HeroIsNotChosen?.Invoke();
            return;
        }

        missionData.SetNewState(MissionState.TemporaryBlocked);
        _tempMissionData = missionData;
        FightPanel.Initialize(missionData);
        StartButtonClickConfirmed?.Invoke();

        _heroesController.DisablePossibilityToChooseHero();

        FightPanel.FinishButtonCllicked += OnFinishButtonClick;
        FightPanel.Disabled += OnFightPanelDisable;
    }

    private void OnFinishButtonClick()
    {
        FinishButtonClicked?.Invoke();
        _tempMissionData.SetNewState(MissionState.Completed);

        _heroesController.SetExperienseForCompletedMission(_tempMissionData);
        _heroesController.EnablePossibilityToChooseHero();
        _heroesController.SelectedHeroModel.UnSelectHero();
    }
}