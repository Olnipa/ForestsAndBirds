using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(HeroToggle))]
[RequireComponent(typeof(Image))]
public class HeroView : MonoBehaviour
{
    [SerializeField] private LockedHeroView _lockedView;
    [SerializeField] private UnlockedHeroView _unlockedView;

    [SerializeField] protected bool IsAvailable;
    [SerializeField] protected TextMeshProUGUI ExperienceValue;
    [SerializeField] protected TextMeshProUGUI Name;

    private HeroModel _heroModel;
    private Image _rayCastImage;
    private HeroToggle _heroToggle;

    public event UnityAction<HeroModel> HeroViewSelected;
    public event UnityAction HeroViewUnSelected;
    public event UnityAction<HeroView> Destroyed;

    private void OnDestroy()
    {
        _heroModel.ExperienceUpdated -= UpdateExperience;
        _heroModel.Unselected -= OnUnselected;
        _heroToggle.onValueChanged.RemoveListener(OnSelectValueChanged);
        Destroyed?.Invoke(this);
    }

    public void Initialize(HeroModel heroModel, ToggleGroup toggleGroup)
    {
        _rayCastImage = GetComponent<Image>();
        _heroToggle = GetComponent<HeroToggle>();
        _heroToggle.group = toggleGroup;
        _heroModel = heroModel;
        UpdateInfo();

        _heroModel.ExperienceUpdated += UpdateExperience;
        _heroModel.Unlocked += Unlock;
        _heroModel.Unselected += OnUnselected;
        _heroToggle.onValueChanged.AddListener(OnSelectValueChanged);
    }

    public void SwitchRayCastTarget(bool isOn)
    {
        _rayCastImage.raycastTarget = isOn;
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        _lockedView.gameObject.SetActive(!isAvailable);
        _unlockedView.gameObject.SetActive(isAvailable);
        _heroToggle.enabled = isAvailable;

        if (isAvailable == false)
            _heroToggle.isOn = false;
    }

    public void UpdateInfo()
    {
        Name.text = _heroModel.Name;
        UpdateExperience();
        SetAvailability(_heroModel.IsUnlocked);
    }

    private void OnUnselected()
    {
        _heroToggle.isOn = false;
    }

    private void Unlock()
    {
        SetAvailability(true);
        _heroModel.Unlocked -= Unlock;
    }

    private void UpdateExperience()
    {
        ExperienceValue.text = Convert.ToString(_heroModel.Experience);
    }

    private void OnSelectValueChanged(bool isChecked)
    {
        if (isChecked)
            HeroViewSelected?.Invoke(_heroModel);
        else
            HeroViewUnSelected?.Invoke();
    }
}