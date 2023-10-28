using System;
using UnityEngine;

public class MissionButton : IDisposable
{
    protected MissionPanel LeftMissionPanel;
    protected MissionData FirstMissionData;

    public event Action StateUpdated;

    public MissionButton(MissionData firstMission, MissionPanel leftMissionPanel)
    {
        FirstMissionData = firstMission;
        LeftMissionPanel = leftMissionPanel;

        FirstMissionData.StateUpdated += OnMissionStateUpdated;
    }

    public virtual void OnMissionInfoButtonClick()
    {
        LeftMissionPanel.Initialize(FirstMissionData);
    }

    public virtual MissionState GetState()
    {
        return FirstMissionData.State;
    }

    public virtual string GetID()
    {
        return FirstMissionData.ID;
    }

    public Vector2 GetAnchorsPosition()
    {
        return FirstMissionData.ButtonAnchorsPosition;
    }

    public virtual void Dispose()
    {
        FirstMissionData.StateUpdated -= OnMissionStateUpdated;
    }

    protected void OnMissionStateUpdated()
    {
        StateUpdated?.Invoke();
    }
}