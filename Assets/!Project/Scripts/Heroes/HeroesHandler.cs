using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroesHandler : MonoBehaviour
{
    [SerializeField] private GameObject _heroViewPrefab;
    [SerializeField] private ToggleGroup _toggleGroupContainer;
    
    private readonly List<HeroView> _heroesView = new List<HeroView>();
    private readonly Dictionary<Type, HeroModel> _heroesModel = new Dictionary<Type, HeroModel>() 
    {
        { typeof(Hawk), new Hawk() },
        { typeof(Gull), new Gull() },
        { typeof(Crow), new Crow() },
        { typeof(Owl), new Owl() }
    };

    public HeroModel CurrentHeroModel { get; private set; }

    private void Start()
    {
        foreach (var hero in _heroesModel)
        {
            hero.Value.HeroModelSelected += SetCurrentHero;
            hero.Value.HeroModelUnSelected += RemoveCurrentHero;
        }

        InstantiateHeroViews();
    }

    private void InstantiateHeroViews()
    {
        foreach (var hero in _heroesModel)
        {
            GameObject heroViewGameObject = Instantiate(_heroViewPrefab, _toggleGroupContainer.transform);
            HeroView heroView = heroViewGameObject.GetComponent<HeroView>();
            heroView.SetToggleGroup(_toggleGroupContainer);
            heroView.UpdateInfo(hero.Value.Name, hero.Value.Experience, hero.Value.IsUnlocked);

            hero.Value.SetHeroView(heroView);
            _heroesView.Add(heroView);
        }
    }

    private void SetCurrentHero(HeroModel hero)
    {
        CurrentHeroModel = hero;
    }

    private void RemoveCurrentHero()
    {
        CurrentHeroModel = null;
    }

    private void OnDestroy()
    {
        foreach (var heroModel in _heroesModel)
        {
            heroModel.Value.HeroModelSelected -= SetCurrentHero;
            heroModel.Value.HeroModelUnSelected -= RemoveCurrentHero;
        }
    }

    public void BlockPossibilityToChooseHero()
    {
        foreach (HeroView hero in _heroesView)
        {
            hero.SwitchRayCastTarget(false);
        }
    }

    public void AllowPossibilityToChoseHero()
    {
        foreach (HeroView hero in _heroesView)
        {
            hero.SwitchRayCastTarget(true);
        }
    }

    public void SetExperienseForCompletedMission(MissionData missionData)
    {
        if (missionData.State != MissionState.Completed)
            return;

        CurrentHeroModel.AddExperience(missionData.GetExperienceForMissionByHeroType(typeof(HeroModel)));

        foreach (var heroKeyValuePair in _heroesModel)
        {
            int experience = missionData.GetExperienceForMissionByHeroType(heroKeyValuePair.Key);

            if (missionData.GetExperienceForMissionByHeroType(heroKeyValuePair.Key) != 0 && heroKeyValuePair.Value.IsUnlocked)
                heroKeyValuePair.Value.AddExperience(experience);
        }
    }

    public void TryUnlockHeroesByType(List<Type> heroTypesToUnlock)
    {
        if (heroTypesToUnlock == null)
            return;

        foreach (Type heroType in heroTypesToUnlock)
        {
            HeroModel hero = _heroesModel.FirstOrDefault(hero => hero.Key == heroType).Value;

            if (hero == null || hero.IsUnlocked)
                continue;

            hero.UnLock();
            break;
        }
    }
}