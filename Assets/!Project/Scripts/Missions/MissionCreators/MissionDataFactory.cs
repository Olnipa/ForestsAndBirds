using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionDataFactory
{
    private const int IDPositionIndex = 0;
    private const int StateIndex = 1;
    private const int NameIndex = 2;
    private const int ButtonPositionXIndex = 3;
    private const int ButtonPositionYIndex = 4;
    private const int ExclusiveMissionIndex = 5;
    private const int MissionsIDToUnlock = 6;
    private const int DescriptionIndex = 7;
    private const int MainMissionTextIndex = 8;
    private const int PlayerSideIndex = 9;
    private const int EnemySideIndex = 10;
    private const int HeroToUnlockIndex = 11;
    private const int CurrentHeroExperienceIndex = 12;
    private const int HawkExperienceIndex = 13;
    private const int CrowExperienceIndex = 14;
    private const int OwlExperienceIndex = 15;
    private const int GullExperienceIndex = 16;

    private const char SeparatorOR = '|';
    private const char SeparatorAND = '&';

    public MissionData CreateMissionData(string[] dataLine)
    {
        string id = dataLine[IDPositionIndex];
        int state = GetNumberByString(dataLine[StateIndex]);
        Vector2 position = GetPosition(dataLine[ButtonPositionXIndex], dataLine[ButtonPositionYIndex]);
        string exclusiveMissionID = dataLine[ExclusiveMissionIndex];
        List<string> missionsIDToUnlock = GetMissionsIDToUnlock(dataLine[MissionsIDToUnlock]);
        List<Type> heroesToUnlock = GetTypesOfHeroesToUnlock(dataLine[HeroToUnlockIndex]);
        Dictionary<Type, int> experienceForHeroes = GetMissionExperience(dataLine);

        return new MissionData(id, state, dataLine[NameIndex], position, exclusiveMissionID, missionsIDToUnlock, dataLine[DescriptionIndex],
            dataLine[MainMissionTextIndex], dataLine[PlayerSideIndex], dataLine[EnemySideIndex], heroesToUnlock, experienceForHeroes);
    }

    private int GetNumberByString(string stringState)
    {
        if (int.TryParse(stringState, out int state))
            return state;
        else if (stringState == "")
            return 0;

        throw new InvalidOperationException($"Value {stringState} is not a number.");
    }

    private Vector2 GetPosition(string stringPositionX, string stringPositionY)
    {
        if (!float.TryParse(stringPositionX, out float positsionX) || !float.TryParse(stringPositionY, out float positsionY))
            throw new InvalidOperationException($"PositionX {stringPositionX} or PositionY {stringPositionY} is not a number.");

        if (positsionX < 0 || positsionX > 1 || positsionY < 0 || positsionY > 1)
            throw new InvalidOperationException($"PositionX {stringPositionX} or PositionY {stringPositionY} is out of range.");

        return new Vector2(positsionX, positsionY);
    }

    private List<Type> GetTypesOfHeroesToUnlock(string stringHeroesToUnlock)
    {
        List<string> heroesNamesToUnlock = stringHeroesToUnlock.Split(SeparatorOR).ToList();

        List<Type> typesOfHeroesToUnlock = new List<Type>();

        foreach (string heroToUnlock in heroesNamesToUnlock)
        {
            Type type = Type.GetType(heroToUnlock);

            if (type != null)
                typesOfHeroesToUnlock.Add(type);
        }

        return typesOfHeroesToUnlock;
    }

    private List<string> GetMissionsIDToUnlock(string stringMissionsIDToUnlock)
    {
        return stringMissionsIDToUnlock.Split(SeparatorAND).ToList();
    }

    private Dictionary<Type, int> GetMissionExperience(string[] dataLine)
    {
        Dictionary<Type, int> missionExperienceForHeroes = new Dictionary<Type, int>()
        {
            { typeof(HeroModel), GetNumberByString(dataLine[CurrentHeroExperienceIndex]) },
            { typeof(Hawk), GetNumberByString(dataLine[HawkExperienceIndex]) },
            { typeof(Crow), GetNumberByString(dataLine[CrowExperienceIndex]) },
            { typeof(Owl), GetNumberByString(dataLine[OwlExperienceIndex]) },
            { typeof(Gull), GetNumberByString(dataLine[GullExperienceIndex]) }
        };

        return missionExperienceForHeroes;
    }
}