using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelsCloser : MonoBehaviour, IUICloser
{
    [SerializeField] private Button _closePanelsButton;

    private readonly List<IPanelUI> _panels = new List<IPanelUI>();

    public event Action AllPanelsClosed;

    private void OnDestroy()
    {
        _closePanelsButton.onClick.RemoveListener(CloseAllPanels);
    }

    public void Initialize()
    {
        _closePanelsButton.onClick.AddListener(CloseAllPanels);
        _panels.AddRange(GetComponentsInChildren<IPanelUI>());
        CloseAllPanels();
    }

    public void CloseAllPanels()
    {
        foreach (var panel in _panels)
        {
            panel.DisablePanel();
        }

        AllPanelsClosed?.Invoke();
    }
}