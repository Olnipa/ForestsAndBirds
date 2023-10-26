using System.Collections.Generic;

public class UIStateChanger
{
    private List<MissionButtonView> _missionButtonViews;
    private FightPanelInitializer _fightPanelInitializer;
    private StartMissionInitializer _startMissionInitializer;
    private IUICloser _uiCloser;

    private UIStateMachine _uiStateMachine = new UIStateMachine();

    public UIStateChanger(List<MissionButtonView> missionButtonViews, FightPanelInitializer fightPanelInitializer, IUICloser uiCloser, StartMissionInitializer startMissionInitializer)
    {
        _missionButtonViews = missionButtonViews;
        _fightPanelInitializer = fightPanelInitializer;
        _uiCloser = uiCloser;
        _startMissionInitializer = startMissionInitializer;
        _uiStateMachine.ChangeState(new MapUIState(_uiCloser));

        foreach (MissionButtonView missionButtonView in _missionButtonViews)
        {
            missionButtonView.ButtonClicked += OnMissionButtonClick;
            missionButtonView.Destroyed += OnMissionButtonViewDestroy;
        }

        _startMissionInitializer.StartButtonClickConfirmed += OnConfirmedStartButtonClick;
        _fightPanelInitializer.MissionCompleted += OnFinishButtonClick;
        _startMissionInitializer.Disposed += OnMissionPanelsControllerDisposed;
    }

    private void OnMissionButtonClick(MissionButtonModel missionButton)
    {
        if (missionButton is DoubleMissionButtonModel)
            _uiStateMachine.ChangeState(new DoubleMissionInfoUIState(_startMissionInitializer.LeftMissionPanel, _startMissionInitializer.RightMissionPanel));
        else
            _uiStateMachine.ChangeState(new SingleMissionInfoUIState(_startMissionInitializer.LeftMissionPanel));
    }

    private void OnConfirmedStartButtonClick(MissionData missionData)
    {
        _uiStateMachine.ChangeState(new FightUIState(_fightPanelInitializer.FightPanel));
    }

    private void OnFinishButtonClick(MissionData missionData)
    {
        _uiStateMachine.ChangeState(new MapUIState(_uiCloser));
    }

    private void OnMissionButtonViewDestroy(MissionButtonView missionButtonView)
    {
        missionButtonView.ButtonClicked -= OnMissionButtonClick;
        missionButtonView.Destroyed -= OnMissionButtonViewDestroy;
    }

    private void OnMissionPanelsControllerDisposed()
    {
        _startMissionInitializer.StartButtonClickConfirmed -= OnConfirmedStartButtonClick;
        _fightPanelInitializer.MissionCompleted -= OnFinishButtonClick;
        _startMissionInitializer.Disposed -= OnMissionPanelsControllerDisposed;
    }
}
