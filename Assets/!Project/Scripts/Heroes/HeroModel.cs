using System;

public abstract class HeroModel
{
    public int Experience { get; protected set; }
    public string Name { get; protected set; }
    public bool IsAvailable { get; private set; }

    private HeroView _heroView;

    public event Action<HeroModel> HeroModelSelected;
    public event Action HeroModelUnSelected;

    public HeroModel(bool isAvailableAtStart)
    {
        IsAvailable = isAvailableAtStart;
    }

    public void SetHeroView(HeroView heroView)
    {
        _heroView = heroView;

        _heroView.HeroViewSelected += OnHeroSelected;
        _heroView.HeroViewUnSelected += OnHeroUnSelected;
        _heroView.Destroyed += OnHeroViewDestroy;
    }

    private void OnHeroViewDestroy()
    {
        _heroView.Destroyed -= OnHeroViewDestroy;
        _heroView.HeroViewSelected -= OnHeroSelected;
        _heroView.HeroViewUnSelected -= OnHeroUnSelected;
    }

    private void OnHeroSelected()
    {
        HeroModelSelected?.Invoke(this);
    }

    private void OnHeroUnSelected()
    {
        HeroModelUnSelected?.Invoke();
    }

    public void UnLock()
    {
        if (IsAvailable == false)
        {
            IsAvailable = true;
            _heroView.SetAvailability(true);
        }
    }

    public void AddExperience(int experience)
    {
        if (experience <= 0)
            return;

        Experience += experience;
        _heroView.UpdateExperience(Experience);
    }
}