using System;
using System.Collections.Generic;
using System.Linq;

public class MissionsUnlocker : IDisposable
{
    private List<MissionData> _missionsData;

    public event Action<MissionData> MissionUnlocked;

    public MissionsUnlocker(List<MissionData> missionsData)
    {
        _missionsData = missionsData;

        if (_missionsData == null)
            throw new NullReferenceException();

        foreach (var missionData in _missionsData)
        {
            missionData.Completed += UnlockMissions;
        }
    }

    public void Dispose()
    {
        foreach (var missionData in _missionsData)
        {
            missionData.Completed -= UnlockMissions;
        }
    }

    private void UnlockMissions(MissionData completedMissionData)
    {
        foreach (var missionIDToUnlock in completedMissionData.MissionsIDToUnlock)
        {
            MissionData missionToUnlock = _missionsData.FirstOrDefault(missionData => missionData.ID == missionIDToUnlock);

            if (missionToUnlock != null && missionToUnlock.State == MissionState.Blocked)
            {
                missionToUnlock.SetNewState(MissionState.Active);
                MissionUnlocked?.Invoke(completedMissionData);
            }
        }
    }
}