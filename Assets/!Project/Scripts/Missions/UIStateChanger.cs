using System.Collections.Generic;

public class UIStateChanger
{
    private List<MissionButtonView> _missionButtonViews;
    private MissionPanelsController _missionPanelsController;

    private UIStateMachine _uiStateMachine = new UIStateMachine();

    public UIStateChanger(List<MissionButtonView> missionButtonViews, MissionPanelsController missionPanelsController)
    {
        _missionPanelsController = missionPanelsController;
        _missionButtonViews = missionButtonViews;
        _uiStateMachine.ChangeState(new MapUIState(_missionPanelsController));

        foreach (MissionButtonView missionButtonView in _missionButtonViews)
        {
            missionButtonView.ButtonClicked += OnMissionButtonClick;
            missionButtonView.Destroyed += RemoveMissionButtonListeners;
        }

        _missionPanelsController.StartButtonClickConfirmed += OnConfirmedStartButtonClick;
        _missionPanelsController.FinishButtonClicked += OnFinishButtonClick;
        _missionPanelsController.Disposed += RemovePanelsListeners;
    }

    private void OnMissionButtonClick(MissionButtonModel missionButton)
    {
        if (missionButton is DoubleMissionButtonModel)
            _uiStateMachine.ChangeState(new DoubleMissionInfoUIState(_missionPanelsController.LeftMissionPanel, _missionPanelsController.RightMissionPanel));
        else
            _uiStateMachine.ChangeState(new SingleMissionInfoUIState(_missionPanelsController.LeftMissionPanel));
    }

    private void OnConfirmedStartButtonClick()
    {
        _uiStateMachine.ChangeState(new FightUIState(_missionPanelsController.FightPanel));
    }

    private void OnFinishButtonClick()
    {
        _uiStateMachine.ChangeState(new MapUIState(_missionPanelsController));
    }

    private void RemoveMissionButtonListeners(MissionButtonView missionButtonView)
    {
        missionButtonView.ButtonClicked -= OnMissionButtonClick;
        missionButtonView.Destroyed -= RemoveMissionButtonListeners;
    }

    private void RemovePanelsListeners()
    {
        _missionPanelsController.StartButtonClickConfirmed -= OnConfirmedStartButtonClick;
        _missionPanelsController.FinishButtonClicked -= OnFinishButtonClick;
        _missionPanelsController.Disposed -= RemovePanelsListeners;
    }
}
