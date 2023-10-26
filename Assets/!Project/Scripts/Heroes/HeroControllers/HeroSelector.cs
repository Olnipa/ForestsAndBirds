using System.Collections.Generic;

public class HeroSelector
{
    private List<HeroView> _heroesView;

    public HeroModel SelectedHero { get; private set; }

    public HeroSelector(List<HeroView> heroViews)
    {
        _heroesView = heroViews;

        foreach (var heroView in _heroesView)
        {
            heroView.HeroViewSelected += SetCurrentHero;
            heroView.HeroViewUnSelected += RemoveCurrentHero;
            heroView.Destroyed += OnHeroViewDestroy;
        }
    }

    private void SetCurrentHero(HeroModel hero)
    {
        SelectedHero = hero;
    }

    private void RemoveCurrentHero()
    {
        SelectedHero = null;
    }

    private void OnHeroViewDestroy(HeroView heroView)
    {
        heroView.Destroyed -= OnHeroViewDestroy;
        heroView.HeroViewSelected -= SetCurrentHero;
        heroView.HeroViewUnSelected -= RemoveCurrentHero;
    }
}
