using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionPanelHandler : MonoBehaviour, IUICloser
{
    [SerializeField] private Button _closePanelsButton;
    [SerializeField] private MissionPanel _leftMissionPanel;
    [SerializeField] private MissionPanel _rightMissionPanel;
    [SerializeField] private FightPanel _fightPanel;

    [SerializeField] private HeroesHandler _heroesHandler;

    private UIStateMachine _uiStateMachine;
    private MissionData _tempMissionData;
    private List<IPanelUI> _panels = new List<IPanelUI>();

    public event UnityAction HeroIsNotChosen;

    private void Start()
    {
        _uiStateMachine = new UIStateMachine();
        _uiStateMachine.Initialize(new MapUIState(this));
        _panels.AddRange(GetComponentsInChildren<IPanelUI>());
        CloseAllPanels();

        _leftMissionPanel.Enabled += OnLeftMissionPanelEnabled;
        _leftMissionPanel.Enabled += OnRightMissionPanelEnabled;
    }

    private void OnDestroy()
    {
        _leftMissionPanel.Enabled -= OnLeftMissionPanelEnabled;
        _leftMissionPanel.Enabled -= OnRightMissionPanelEnabled;
    }

    public void OnLeftMissionPanelEnabled()
    {
        _leftMissionPanel.StartButtonClicked += OnStartButtonClick;
        _leftMissionPanel.Disabled += OnLeftMissionPanelDisable;
    }

    public void OnRightMissionPanelEnabled()
    {
        _rightMissionPanel.StartButtonClicked += OnStartButtonClick;
        _rightMissionPanel.Disabled += OnRightMissionPanelDisable;
    }

    private void OnEnable()
    {
        _closePanelsButton.onClick.AddListener(CloseAllPanels);
        _closePanelsButton.onClick.AddListener(_heroesHandler.AllowPossibilityToChoseHero);
    }

    private void OnDisable()
    {
        _closePanelsButton.onClick.RemoveListener(CloseAllPanels);
        _closePanelsButton.onClick.RemoveListener(_heroesHandler.AllowPossibilityToChoseHero);
    }

    private void OnLeftMissionPanelDisable()
    {
        _leftMissionPanel.StartButtonClicked -= OnStartButtonClick;
        _leftMissionPanel.Disabled -= OnLeftMissionPanelDisable;
    }

    private void OnRightMissionPanelDisable()
    {
        _rightMissionPanel.StartButtonClicked -= OnStartButtonClick;
        _leftMissionPanel.Disabled -= OnRightMissionPanelDisable;
    }

    private void OnFightPanelDisable()
    {
        _fightPanel.FinishButtonCllicked -= OnFinishButtonClick;
        _fightPanel.Disabled -= OnFightPanelDisable;
    }

    private void OnStartButtonClick(MissionData missionData)
    {
        if (_heroesHandler.CurrentHeroModel == null)
        {
            HeroIsNotChosen?.Invoke();
            return;
        }

        missionData.SetNewState(MissionState.TemporaryBlocked);
        _tempMissionData = missionData;
        _fightPanel.Initialize(missionData);
        _uiStateMachine.ChangeState(new FightUIState(_fightPanel));

        _heroesHandler.BlockPossibilityToChooseHero();

        _fightPanel.FinishButtonCllicked += OnFinishButtonClick;
        _fightPanel.Disabled += OnFightPanelDisable;
    }

    private void OnFinishButtonClick()
    {
        _tempMissionData.SetNewState(MissionState.Completed);
        _uiStateMachine.ChangeState(new MapUIState(this));

        _heroesHandler.SetExperienseForCompletedMission(_tempMissionData);
        _heroesHandler.AllowPossibilityToChoseHero();
        _heroesHandler.CurrentHeroModel.UnSelectHeroView();
    }

    public void CloseAllPanels()
    {
        foreach (var panel in _panels)
        {
            panel.DisablePanel();
        }
    }
}