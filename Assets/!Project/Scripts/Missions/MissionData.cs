using System;
using System.Collections.Generic;
using UnityEngine;

public class MissionData
{
    public readonly string ID;
    public readonly string Name;
    public readonly Vector2 ButtonAnchorsPosition;
    public readonly string MutuallyExclusiveID;
    public readonly List<string> MissionsIDToUnlock;
    public readonly string Description;
    public readonly string MissionText;
    public readonly string PlayerSide;
    public readonly string EnemySide;
    public readonly List<Type> HeroesToUnlock;
    public readonly Dictionary<Type, int> ExperienceForHeroes;

    public MissionState State { get; protected set; }

    public MissionData(string id, int state, string name, Vector2 position, string exclusiveMissionID, List<string> missionsIDToUnlock, string descriptionText, 
        string missionText, string playerSide, string enemySide, List<Type> heroesToUnlock, Dictionary<Type, int> experienceForHeroes)
    {
        ID = id;
        State = GetMissionState(state);
        Name = name;
        ButtonAnchorsPosition = position;
        MutuallyExclusiveID = exclusiveMissionID;
        MissionsIDToUnlock = missionsIDToUnlock;
        Description = descriptionText;
        MissionText = missionText;
        PlayerSide = playerSide;
        EnemySide = enemySide;
        HeroesToUnlock = heroesToUnlock;
        ExperienceForHeroes = experienceForHeroes;
    }

    public event Action StateUpdated;
    public event Action<MissionData> Completed;

    public void SetNewState(MissionState newState)
    {
        if (State == newState)
            return;

        State = newState;
        StateUpdated?.Invoke();

        if (newState == MissionState.Completed)
            Completed?.Invoke(this);
    }

    public int GetExperienceForMissionByHeroType(Type heroType)
    {
        foreach (var experienceForHero in ExperienceForHeroes)
        {
            if (heroType == experienceForHero.Key)
                return experienceForHero.Value;
        }

        throw new InvalidOperationException($"There are no Hero type {heroType.Name} in Experience for Heroes list");
    }

    private MissionState GetMissionState(int stateIndex)
    {
        if (stateIndex >= 0 && stateIndex < Enum.GetNames(typeof(MissionState)).Length)
            return (MissionState)stateIndex;

        throw new InvalidOperationException($"State {stateIndex} is out of range.");
    }
}