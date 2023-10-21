using System;

public abstract class HeroModel
{
    public int Experience { get; protected set; }
    public string Name { get; protected set; }
    public bool IsUnlocked { get; private set; }

    private HeroView _heroView;

    public event Action<HeroModel> HeroModelSelected;
    public event Action HeroModelUnSelected;
    public event Action HeroUnlocked;

    public HeroModel(bool isAvailableAtStart)
    {
        IsUnlocked = isAvailableAtStart;
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

    public void OnHeroUnSelected()
    {
        HeroModelUnSelected?.Invoke();
    }

    public void UnSelectHeroView()
    {
        _heroView.HeroToggle.isOn = false;
    }

    public void UnLock()
    {
        if (IsUnlocked == false)
        {
            IsUnlocked = true;
            _heroView.SetAvailability(true);
            HeroUnlocked?.Invoke();
        }
    }

    public void AddExperience(int experience)
    {
        if (experience == 0)
            return;

        Experience += experience;
        _heroView.UpdateExperience(Experience);
    }

    public void SetHeroView(HeroView heroView)
    {
        _heroView = heroView;

        _heroView.HeroViewSelected += OnHeroSelected;
        _heroView.HeroViewUnSelected += OnHeroUnSelected;
        _heroView.Destroyed += OnHeroViewDestroy;
    }
}