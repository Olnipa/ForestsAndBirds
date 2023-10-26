using System;
using UnityEngine;

public class MissionButtonModel : IDisposable
{
    protected MissionPanel LeftMissionPanel;

    public MissionData FirstMissionData { get; private set; }

    public event Action StateUpdated;

    public MissionButtonModel(MissionData firstMission, MissionPanel leftMissionPanel)
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