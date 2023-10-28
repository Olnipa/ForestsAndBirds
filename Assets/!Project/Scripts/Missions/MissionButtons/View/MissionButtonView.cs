using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionButtonView : Button
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _id;
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;

    private MissionButton _missionButton;
    
    private StateMachine _missionStateMachine = new MissionStateMachine();
    private MissionStateFactory _stateFactory;
    private MissionState _state;

    private const string HighlightedTrigger = "HighlightedTrigger";
    private const string NormalTrigger = "NormalTrigger";

    public event Action<MissionButtonView> Destroyed;
    public event Action<MissionButton> ButtonClicked;

    public void InitializeView(MissionButton missionButton)
    {
        _missionButton = missionButton;

        _missionButton.StateUpdated += OnStateUpdated;
        SetID(_missionButton.GetID());

        onClick.AddListener(OnMissionButtonClick);

        transform.localPosition = Vector3.zero;
        _rectTransform.anchorMin = _missionButton.GetAnchorsPosition();
        _rectTransform.anchorMax = _rectTransform.anchorMin;

        _stateFactory = new MissionStateFactory(_animator, _image);
        _missionStateMachine.ChangeState(_stateFactory.CreateState(_state));
        UpdateState(_missionButton.GetState());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _missionButton.StateUpdated -= OnStateUpdated;
        onClick.RemoveListener(OnMissionButtonClick);
        Destroyed?.Invoke(this);
    }

    public void OnMissionButtonClick()
    {
        _animator.SetTrigger(HighlightedTrigger);
        _missionButton.OnMissionInfoButtonClick();
        ButtonClicked?.Invoke(_missionButton);
    }

    public void UpdateState(MissionState newState)
    {
        _missionStateMachine.ChangeState(_stateFactory.CreateState(newState));
    }

    public void SetID(string id)
    {
        _id.text = id;
    }

    private void OnStateUpdated()
    {
        UpdateState(_missionButton.GetState());
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        _animator.ResetTrigger(NormalTrigger);
        _animator.SetTrigger(HighlightedTrigger);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        _animator.ResetTrigger(HighlightedTrigger);
        _animator.SetTrigger(NormalTrigger);
    }
}