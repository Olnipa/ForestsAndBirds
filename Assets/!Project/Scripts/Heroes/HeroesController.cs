using System;
using System.Collections.Generic;
using System.Linq;

public class HeroesController
{
    private Dictionary<Type, HeroModel> _heroesModel;

    private List<HeroView> _heroesView;

    public HeroModel SelectedHeroModel { get; private set; }

    public HeroesController(Dictionary<Type, HeroModel> heroModels, List<HeroView> heroViews)
    {
        _heroesModel = heroModels;
        _heroesView = heroViews;

        foreach (var heroView in _heroesView)
        {
            heroView.HeroViewSelected += SetCurrentHero;
            heroView.HeroViewUnSelected += RemoveCurrentHero;
            heroView.Destroyed += OnHeroViewDestroy;
        }
    }

    public void DisablePossibilityToChooseHero()
    {
        foreach (HeroView hero in _heroesView)
        {
            hero.SwitchRayCastTarget(false);
        }
    }

    public void EnablePossibilityToChooseHero()
    {
        foreach (HeroView hero in _heroesView)
        {
            hero.SwitchRayCastTarget(true);
        }
    }

    public HeroModel GetHeroByType(Type heroType)
    {
        return _heroesModel.FirstOrDefault(hero => hero.Key == heroType).Value;
    }

    private void SetCurrentHero(HeroModel hero)
    {
        SelectedHeroModel = hero;
    }

    private void RemoveCurrentHero()
    {
        SelectedHeroModel = null;
    }

    private void OnHeroViewDestroy(HeroView heroView)
    {
        heroView.Destroyed -= OnHeroViewDestroy;
        heroView.HeroViewSelected -= SetCurrentHero;
        heroView.HeroViewUnSelected -= RemoveCurrentHero;
    }
}

public class HeroesUnlocker : IDisposable
{
    private MissionsUnlocker _missionsUnlocker;
    private HeroesController _heroesController;

    public HeroesUnlocker(MissionsUnlocker missionsUnlocker, HeroesController heroesController, Dictionary<Type, HeroModel> heroesModel)
    {
        _missionsUnlocker = missionsUnlocker;
        _heroesController = heroesController;

        _missionsUnlocker.MissionUnlocked += TryUnlockHero;
        _missionsUnlocker.MissionUnlocked += SetExperienseForCompletedMission;
    }

    public void Dispose()
    {
        _missionsUnlocker.MissionUnlocked -= TryUnlockHero;
        _missionsUnlocker.MissionUnlocked -= SetExperienseForCompletedMission;
    }

    private void TryUnlockHero(MissionData missionData)
    {
        if (missionData.HeroesToUnlock == null)
            return;

        foreach (Type heroType in missionData.HeroesToUnlock)
        {
            HeroModel hero = _heroesController.GetHeroByType(heroType);

            if (hero == null || hero.IsUnlocked)
                continue;

            hero.UnLock();
            break;
        }
    }

    private void SetExperienseForCompletedMission(MissionData missionData)
    {
        if (missionData.State != MissionState.Completed)
            return;

        _heroesController.SelectedHeroModel.AddExperience(missionData.GetExperienceForMissionByHeroType(typeof(HeroModel)));

        foreach (var heroKeyValuePair in _heroesModel)
        {
            int experience = missionData.GetExperienceForMissionByHeroType(heroKeyValuePair.Key);

            if (experience != 0 && heroKeyValuePair.Value.IsUnlocked)
                heroKeyValuePair.Value.AddExperience(experience);
        }
    }
}

public class ExperienceDistributor
{
    private HeroesController _heroesController;
    private Dictionary<Type, HeroModel> _heroesModel;

    private void SetExperienseForCompletedMission(MissionData missionData)
    {
        if (missionData.State != MissionState.Completed)
            return;

        _heroesController.SelectedHeroModel.AddExperience(missionData.GetExperienceForMissionByHeroType(typeof(HeroModel)));

        foreach (var heroKeyValuePair in _heroesModel)
        {
            int experience = missionData.GetExperienceForMissionByHeroType(heroKeyValuePair.Key);

            if (experience != 0 && heroKeyValuePair.Value.IsUnlocked)
                heroKeyValuePair.Value.AddExperience(experience);
        }
    }
}