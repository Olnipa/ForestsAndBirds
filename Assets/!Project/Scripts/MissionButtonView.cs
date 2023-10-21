using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]

public class MissionButtonView : MonoBehaviour
{
    [SerializeField] private Color _activeButtonColor;
    [SerializeField] private Color _temporaryBlockedButtonColor;
    [SerializeField] private Color _completedButtonColor;
    
    private MissionStateMachine _stateMachine = new MissionStateMachine();
    private MissionStateFactory _stateFactory;
    private MissionState _state;

    public TextMeshProUGUI ID { get; private set; }
    public Image Image { get; private set; }
    public Animator Animator { get; private set; }
    public Button Button { get; private set; }
    public RectTransform RectTransform { get; private set; }

    public event Action Destroyed;

    private void Awake()
    {
        ID = GetComponentInChildren<TextMeshProUGUI>();
        Animator = GetComponent<Animator>();
        Image = GetComponent<Image>();
        Button = GetComponent<Button>();
        RectTransform = GetComponent<RectTransform>();
        _stateFactory = new MissionStateFactory(Animator, Image, _activeButtonColor, _temporaryBlockedButtonColor, _completedButtonColor);
        _stateMachine.Initialize(_stateFactory.GetState(_state));
    }

    public void SetNewState(MissionState newState)
    {
        _state = newState;
        _stateMachine.ChangeState(_stateFactory.GetState(newState));
    }

    public void SetID(string id)
    {
        ID.text = id;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}