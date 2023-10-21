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

    private Image _rayCastImage;
    private HeroToggle _heroToggle;

    public event UnityAction HeroViewSelected;
    public event UnityAction HeroViewUnSelected;
    public event UnityAction Destroyed;

    private void OnSelectValueChanged(bool isChecked)
    {
        if (isChecked)
            HeroViewSelected?.Invoke();
        else
            HeroViewUnSelected?.Invoke();
    }

    private void Awake()
    {
        _rayCastImage = GetComponent<Image>();
        _heroToggle = GetComponent<HeroToggle>();
        _heroToggle.onValueChanged.AddListener(OnSelectValueChanged);

        SetAvailability(IsAvailable);
    }

    private void OnDestroy()
    {
        _heroToggle.onValueChanged.RemoveListener(OnSelectValueChanged);
        Destroyed?.Invoke();
    }

    public void SetToggleGroup(ToggleGroup toggleGroup)
    {
        _heroToggle.group = toggleGroup;
    }

    public void SwitchRayCastTarget(bool isOn)
    {
        _rayCastImage.raycastTarget = isOn;
    }

    public void SetAvailability(bool isAvailable)
    {
        _lockedView.gameObject.SetActive(!isAvailable);
        _unlockedView.gameObject.SetActive(isAvailable);
        _heroToggle.enabled = isAvailable;

        if (isAvailable == false)
            _heroToggle.isOn = false;
    }

    public void UpdateInfo(string name, int experience, bool isAvailable)
    {
        Name.text = name;
        UpdateExperience(experience);
        IsAvailable = isAvailable;
        SetAvailability(IsAvailable);
    }

    public void UpdateExperience(int experience)
    {
        ExperienceValue.text = Convert.ToString(experience);
    }
}