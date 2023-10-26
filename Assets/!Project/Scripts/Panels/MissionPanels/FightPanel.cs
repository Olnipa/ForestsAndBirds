using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FightPanel : MonoBehaviour, IPanelUI
{
    [SerializeField] private TextMeshProUGUI _lable;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _heroName;
    [SerializeField] private TextMeshProUGUI _enemyName;
    [SerializeField] private TextMeshProUGUI _missionText;

    [SerializeField] private Button _finishButton;

    public event Action Disabled;
    public event Action FinishButtonCllicked;

    private void OnDisable()
    {
        Disabled?.Invoke();
    }

    private void OnDestroy()
    {
        _finishButton.onClick.RemoveListener(OnFinishButtonClick);
    }

    public void Initialize(MissionData missionData)
    {
        _lable.text = missionData.Name;
        _missionText.text = missionData.MissionText;
        _heroName.text = missionData.PlayerSide;
        _enemyName.text = missionData.EnemySide;
        _finishButton.onClick.AddListener(OnFinishButtonClick);
    }

    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }

    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    private void OnFinishButtonClick()
    {
        FinishButtonCllicked?.Invoke();
    }
}