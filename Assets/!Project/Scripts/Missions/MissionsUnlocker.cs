using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class MissionsUnlocker : IDisposable
{
    private HeroesController _heroesController;
    private List<MissionData> _missionsData;

    public event Action<MissionData> MissionUnlocked;

    public void Initialize(List<MissionData> missionsData, HeroesController heroesController)
    {
        _heroesController = heroesController;
        _missionsData = missionsData;

        if (_missionsData == null)
            throw new NullReferenceException();

        foreach (var missionData in _missionsData)
        {
            missionData.Completed += OnMissionComplete;
        }
    }

    public void Dispose()
    {
        foreach (var missionData in _missionsData)
        {
            missionData.Completed -= OnMissionComplete;
        }
    }

    private void OnMissionComplete(MissionData missionData)
    {
        foreach (var missionIDToUnlock in missionData.MissionsIDToUnlock)
        {
            MissionData missionToUnlock = _missionsData.FirstOrDefault(missionData => missionData.ID == missionIDToUnlock);

            if (missionToUnlock != null && missionToUnlock.State == MissionState.Blocked)
                missionToUnlock.SetNewState(MissionState.Active);
        }

        MissionUnlocked?.Invoke(missionData);
    }
}