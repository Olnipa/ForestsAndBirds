using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroViewFactory : MonoBehaviour, IViewFactory<Dictionary<Type, HeroModel>, List<HeroView>>
{
    [SerializeField] private GameObject _heroViewPrefab;
    [SerializeField] private ToggleGroup _toggleGroupContainer;

    public List<HeroView> CreateViews(Dictionary<Type, HeroModel> heroeModels)
    {
        List<HeroView> heroViews = new List<HeroView>();

        foreach (var heroModel in heroeModels)
        {
            GameObject heroViewGameObject = Instantiate(_heroViewPrefab, _toggleGroupContainer.transform);
            HeroView heroView = heroViewGameObject.GetComponent<HeroView>();
            heroView.Initialize(heroModel.Value, _toggleGroupContainer);

            heroViews.Add(heroView);
        }
        
        return heroViews;
    }
}