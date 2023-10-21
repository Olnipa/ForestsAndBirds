using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroesHandler : MonoBehaviour
{
    [SerializeField] private GameObject _heroViewPrefab;
    [SerializeField] private ToggleGroup _toggleGroupContainer;
    
    private List<HeroView> _heroesView = new List<HeroView>();

    private Dictionary<Type, HeroModel> _heroesModel = new Dictionary<Type, HeroModel>() 
    {
        { typeof(Hawk), new Hawk() },
        { typeof(Gull), new Gull() },
        { typeof(Crow), new Crow() },
        { typeof(Owl), new Owl() }
    };

    public int HeroesCount => _heroesModel.Count;
    public static HeroModel CurrentHeroModel { get; private set; }

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
            heroView.UpdateInfo(hero.Value.Name, hero.Value.Experience, hero.Value.IsAvailable);

            hero.Value.SetHeroView(heroView);
            _heroesView.Add(heroView);
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
}