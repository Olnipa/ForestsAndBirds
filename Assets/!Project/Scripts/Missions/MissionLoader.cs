using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

[RequireComponent(typeof(MissionsUnlocker))]
public class MissionLoader
{
    private MissionDataFactory _missionDataFactory = new MissionDataFactory();

    private const string DataFileDirectory = "Assets/!Project/StreamingAssets/MissionsData.csv";
    private const char CSVSectionSplitter = ';';

    public List<MissionData> GetMissionsData()
    {
        try
        {
            return LoadMissionsData();
        }
        catch(Exception exception)
        {
            Debug.LogException(exception);
            return null;
        }
    }

    private List<MissionData> LoadMissionsData()
    {
        List<MissionData> missionsData = new List<MissionData>();
        string[] missionsDataLines = File.ReadAllLines(DataFileDirectory);

        for (int i = 1; i < missionsDataLines.Length; i++)
        {
            string[] missionDataString = missionsDataLines[i].Split(CSVSectionSplitter);
            missionsData.Add(_missionDataFactory.CreateMissionData(missionDataString));
        }

        string IDwithMissingUnlockedID = HasMissingUnlockedIDs(missionsData);

        if (string.IsNullOrEmpty(HasMissingUnlockedIDs(missionsData)))
            return missionsData;

        throw new Exception($"Mission ID to Unlock {IDwithMissingUnlockedID} is not existed");
    }

    private string HasMissingUnlockedIDs(List<MissionData> missionsData)
    {
        HashSet<string> missionsID = new HashSet<string>(missionsData.Select(missionData => missionData.ID));

        foreach (var missionData in missionsData)
        {
            if (string.IsNullOrEmpty(missionData.MissionsIDToUnlock[0]))
                continue;

            return missionData.MissionsIDToUnlock.FirstOrDefault(idToUnlock => missionsID.Contains(idToUnlock) == false);
        }

        return String.Empty;
    }
}