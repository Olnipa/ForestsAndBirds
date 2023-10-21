using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissionData : IStateChangable
{
    public string ID { get; private set; }
    public MissionState State { get; protected set; }
    public string Name { get; private set; }
    public Vector2 ButtonAnchorsPosition { get; private set; }
    public string ExclusiveMissionID { get; private set; }
    public List<string> MissionsIDToCompleteToBeActive { get; private set; }
    public string Description { get; private set; }
    public string MissionText { get; private set; }
    public string PlayerSide { get; private set; }
    public string EnemySide { get; private set; }
    public List<Type> HeroesToUnlock { get; private set; }
    public Dictionary<Type, int> ExperienceForHeroes { get; private set; }

    public event Action StateUpdated;

    public MissionData(string id, int state, string name, Vector2 position, string exclusiveMissionID, List<string> missionsIDToCompleteToBeActive, string descriptionText, 
        string missionText, string playerSide, string enemySide, List<Type> heroesToUnlock, Dictionary<Type, int> experienceForHeroes)
    {
        ID = id;
        State = GetMissionState(state);
        Name = name;
        ButtonAnchorsPosition = position;
        ExclusiveMissionID = exclusiveMissionID;
        MissionsIDToCompleteToBeActive = missionsIDToCompleteToBeActive;
        Description = descriptionText;
        MissionText = missionText;
        PlayerSide = playerSide;
        EnemySide = enemySide;
        HeroesToUnlock = heroesToUnlock;
        ExperienceForHeroes = experienceForHeroes;
    }

    public MissionState GetMissionState(int stateIndex)
    {
        if (stateIndex >= 0 && stateIndex < Enum.GetNames(typeof(MissionState)).Length)
            return (MissionState)stateIndex;

        throw new InvalidOperationException($"State {stateIndex} is out of range.");
    }

    public void SetNewState(MissionState newState)
    {
        if (State == newState)
            return;

        State = newState;
        StateUpdated?.Invoke();
    }

    public int GetExperienceByType(Type heroType)
    {
        foreach (var experienceForHero in ExperienceForHeroes)
        {
            if (heroType == experienceForHero.Key)
                return experienceForHero.Value;
        }

        throw new InvalidOperationException($"There are no Hero type {heroType.Name} in Experience for Heroes list");
    }
}
