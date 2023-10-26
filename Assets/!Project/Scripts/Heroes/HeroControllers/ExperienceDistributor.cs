using System;
using System.Collections.Generic;

public class ExperienceDistributor : IDisposable
{
    private List<MissionData> _missionsData;
    private HeroSelector _heroSelector;
    private Dictionary<Type, HeroModel> _heroModels;

    public ExperienceDistributor(List<MissionData> missionsData, Dictionary<Type, HeroModel> heroModels, HeroSelector heroSelector)
    {
        _missionsData = missionsData;
        _heroModels = heroModels;
        _heroSelector = heroSelector;

        foreach (var item in _missionsData)
        {
            item.Completed += SetExperienseForCompletedMission;
        }
    }

    public void Dispose()
    {
        foreach (var item in _missionsData)
        {
            item.Completed -= SetExperienseForCompletedMission;
        }
    }

    private void SetExperienseForCompletedMission(MissionData missionData)
    {
        if (missionData.State != MissionState.Completed)
            return;

        _heroSelector.SelectedHero.AddExperience(missionData.GetExperienceForMissionByHeroType(typeof(HeroModel)));

        foreach (var heroKeyValuePair in _heroModels)
        {
            int experience = missionData.GetExperienceForMissionByHeroType(heroKeyValuePair.Key);

            if (experience != 0 && heroKeyValuePair.Value.IsUnlocked)
                heroKeyValuePair.Value.AddExperience(experience);
        }
    }
}