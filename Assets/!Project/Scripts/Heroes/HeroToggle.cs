using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HeroToggle : Toggle
{
    private Animator _animator;

    private const string ToggleDeselectedTrigger = "ToggleDeselected";
    private const string ToggleSelectedTrigger = "ToggleSelected";
    private const string NormalTrigger = "Normal";
    private const string HighlightedTrigger = "Highlighted";

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

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        onValueChanged.AddListener(OnToggleValueChanged);
    }

    protected override void OnDisable()
    {
        onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    public void OnToggleValueChanged(bool isSelected)
    {
        if (isSelected)
            _animator.SetTrigger(ToggleSelectedTrigger);
        else
            _animator.SetTrigger(ToggleDeselectedTrigger);
    }
}