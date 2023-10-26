using System;
using System.Collections.Generic;
using System.Linq;

public class MissionButtonsModelFactory
{
    private List<MissionData> _usedMissionsData;
    private List<MissionData> _missionsData;
    private MissionPanel _leftMissionPanel;
    private MissionPanel _rightMissionPanel;

    public MissionButtonsModelFactory(MissionPanel leftMissionPanel, MissionPanel rightMissionPanel)
    {
        _leftMissionPanel = leftMissionPanel;
        _rightMissionPanel = rightMissionPanel;
        _usedMissionsData = new List<MissionData>();
    }

    public List<MissionButtonModel> CreateMissionButtonModels(List<MissionData> missionsData)
    {
        _missionsData = missionsData; 
        List<MissionButtonModel> missionButtons = new List<MissionButtonModel>();

        foreach (var missionData in _missionsData)
        {
            if (_usedMissionsData.Contains(missionData))
                continue;

            missionButtons.Add(GetButton(missionData));
        }

        _usedMissionsData.Clear();
        return missionButtons;
    }

    private MissionButtonModel GetButton(MissionData missionData)
    {
        if (string.IsNullOrEmpty(missionData.MutuallyExclusiveID))
            return GetSingleMissionButton(missionData);

        return GetDoubleMissionButton(missionData);
    }

    private MissionButtonModel GetDoubleMissionButton(MissionData currentMissionData)
    {
        MissionData mutuallyExclusiveMissionData = GetMissionDataByID(currentMissionData.MutuallyExclusiveID);

        if (mutuallyExclusiveMissionData == null)
            throw new InvalidOperationException($"Exclusive mission with ID {currentMissionData.MutuallyExclusiveID} does not exist.");

        _usedMissionsData.Add(mutuallyExclusiveMissionData);
        _usedMissionsData.Add(currentMissionData);

        return new DoubleMissionButtonModel(currentMissionData, mutuallyExclusiveMissionData, _leftMissionPanel, _rightMissionPanel);
    }

    private MissionButtonModel GetSingleMissionButton(MissionData missionData)
    {
        _usedMissionsData.Add(missionData);
        return new MissionButtonModel(missionData, _leftMissionPanel);
    }

    private MissionData GetMissionDataByID(string id)
    {
        return _missionsData.FirstOrDefault(missionData => missionData.ID == id);
    }
}