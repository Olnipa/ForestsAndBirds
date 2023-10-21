using System;
using UnityEngine;

public class MissionButton : IMissionButton
{
    protected MissionPanel LeftMissionPanelPrefab;
    protected MissionData FirstMissionData;

    protected MissionButtonView ButtonView;

    public event Action<MissionButton> MissionButtonClicked;

    public MissionButton(MissionData firstMission, MissionPanel leftMissionPanelPrefab)
    {
        FirstMissionData = firstMission;
        LeftMissionPanelPrefab = leftMissionPanelPrefab;
    }

    protected virtual void OnMissionInfoButtonClick()
    {
        MissionButtonClicked?.Invoke(this);
        LeftMissionPanelPrefab.Initialize(FirstMissionData);
    }

    protected virtual MissionState GetState()
    {
        return FirstMissionData.State;
    }

    protected virtual string GetID()
    {
        return FirstMissionData.ID;
    }

    private void RemoveListenersOfMissionButtonView()
    {
        ButtonView.Button.onClick.RemoveListener(OnMissionInfoButtonClick);
        FirstMissionData.StateUpdated -= OnFirstMissionDataUpdated;
        ButtonView.Destroyed -= RemoveListenersOfMissionButtonView;
    }

    protected virtual void AddListenerToMissionDataUpdate()
    {
        FirstMissionData.StateUpdated += OnFirstMissionDataUpdated;
    }

    protected void OnFirstMissionDataUpdated()
    {
        ButtonView.SetNewState(FirstMissionData.State);
    }

    public void InitializeView(MissionButtonView buttonView)
    {
        ButtonView = buttonView;
        ButtonView.SetID(GetID());
        ButtonView.SetNewState(GetState());

        ButtonView.Button.onClick.AddListener(OnMissionInfoButtonClick);
        ButtonView.Destroyed += RemoveListenersOfMissionButtonView;

        ButtonView.transform.localPosition = Vector3.zero;
        ButtonView.RectTransform.anchorMin = FirstMissionData.ButtonAnchorsPosition;
        ButtonView.RectTransform.anchorMax = ButtonView.RectTransform.anchorMin;

        AddListenerToMissionDataUpdate();
    }
}
