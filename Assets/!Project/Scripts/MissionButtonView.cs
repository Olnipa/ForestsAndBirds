using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionButtonView : MonoBehaviour
{
    [SerializeField] private Color _activeButtonColor;
    [SerializeField] private Color _temporaryBlockedButtonColor;
    [SerializeField] private Color _completedButtonColor;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _id;
    [SerializeField] private Image _image;
    [SerializeField] private Button _infoButton;
    [SerializeField] private RectTransform _rectTransform;

    private MissionButtonModel _missionButtonModel;
    
    private StateMachine _stateMachine = new MissionStateMachine();
    private MissionStateFactory _stateFactory;
    private MissionState _state;

    public event Action<MissionButtonView> Destroyed;
    public event Action<MissionButtonModel> ButtonClicked;

    public void InitializeView(MissionButtonModel missionButtonModel)
    {
        _missionButtonModel = missionButtonModel;

        _missionButtonModel.StateUpdated += OnStateUpdated;
        SetID(_missionButtonModel.GetID());
        SetNewState(_missionButtonModel.GetState());

        _infoButton.onClick.AddListener(OnMissionButtonClick);

        transform.localPosition = Vector3.zero;
        _rectTransform.anchorMin = _missionButtonModel.GetAnchorsPosition();
        _rectTransform.anchorMax = _rectTransform.anchorMin;

        _stateFactory = new MissionStateFactory(_animator, _image, _activeButtonColor, _temporaryBlockedButtonColor, _completedButtonColor);
        _stateMachine.ChangeState(_stateFactory.CreateState(_state));
    }

    private void OnDestroy()
    {
        _missionButtonModel.StateUpdated -= OnStateUpdated;
        _infoButton.onClick.RemoveListener(OnMissionButtonClick);
        Destroyed?.Invoke(this);
    }

    public void OnMissionButtonClick()
    {
        _missionButtonModel.OnMissionInfoButtonClick();
        ButtonClicked?.Invoke(_missionButtonModel);
    }

    public void SetNewState(MissionState newState)
    {
        _state = newState;
        _stateMachine.ChangeState(_stateFactory.CreateState(newState));
    }

    public void SetID(string id)
    {
        _id.text = id;
    }

    private void OnStateUpdated()
    {
        SetNewState(_missionButtonModel.GetState());
    }
}