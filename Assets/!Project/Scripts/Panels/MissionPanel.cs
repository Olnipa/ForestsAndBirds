using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour, IPanelUI
{
    [SerializeField] private TextMeshProUGUI _lable;
    [SerializeField] private TextMeshProUGUI _textBeforeMission;
    [SerializeField] private Image _image;

    private Button _startButton;

    private MissionData _missionData;

    public event UnityAction<MissionData> StartButtonClicked;
    public event UnityAction Disabled;
    public event UnityAction Enabled;

    public Button StartButton => _startButton;

    private void OnEnable()
    {
        _startButton = GetComponentInChildren<Button>();
        Enabled?.Invoke();
    }

    private void OnDisable()
    {
        Disabled?.Invoke();
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartMissionClick);
    }

    private void OnStartMissionClick()
    {
        StartButtonClicked?.Invoke(_missionData);
    }

    public void Initialize(MissionData missionData)
    {
        _missionData = missionData;
        _lable.text = _missionData.Name;
        _textBeforeMission.text = _missionData.Description;
        _startButton.onClick.AddListener(OnStartMissionClick);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }

    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }
}