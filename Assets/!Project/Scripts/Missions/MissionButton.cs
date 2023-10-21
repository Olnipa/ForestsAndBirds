using System;
using UnityEngine;

public abstract class MissionButton : IMissionButton
{
    protected MissionPanel _leftMissionPanelPrefab;
    protected MissionData _firstMissionData;

    protected MissionButtonView _buttonView;

    public event Action<MissionButton> MissionButtonClicked;

    public MissionButton(MissionData firstMission, MissionPanel leftMissionPanelPrefab)
    {
        _firstMissionData = firstMission;
        _leftMissionPanelPrefab = leftMissionPanelPrefab;
    }

    protected virtual void OnMissionInfoButtonClick()
    {
        MissionButtonClicked?.Invoke(this);
        _leftMissionPanelPrefab.Initialize(_firstMissionData);
    }

    protected virtual MissionState GetState()
    {
        return _firstMissionData.State;
    }

    protected virtual string GetID()
    {
        return _firstMissionData.ID;
    }

    private void RemoveListenersOfMissionButtonView()
    {
        _buttonView.Button.onClick.RemoveListener(OnMissionInfoButtonClick);
        _firstMissionData.StateUpdated -= OnFirstMissionDataUpdated;
        _buttonView.Destroyed -= RemoveListenersOfMissionButtonView;
    }

    protected virtual void AddListenerToMissionDataUpdate()
    {
        _firstMissionData.StateUpdated += OnFirstMissionDataUpdated;
    }

    protected void OnFirstMissionDataUpdated()
    {
        _buttonView.SetNewState(_firstMissionData.State);
    }

    public void InitializeView(MissionButtonView buttonView)
    {
        _buttonView = buttonView;
        _buttonView.SetID(GetID());
        _buttonView.SetNewState(GetState());

        _buttonView.Button.onClick.AddListener(OnMissionInfoButtonClick);
        _buttonView.Destroyed += RemoveListenersOfMissionButtonView;

        _buttonView.transform.localPosition = Vector3.zero;
        _buttonView.RectTransform.anchorMin = _firstMissionData.ButtonAnchorsPosition;
        _buttonView.RectTransform.anchorMax = _buttonView.RectTransform.anchorMin;

        AddListenerToMissionDataUpdate();
    }
}
