using System;

public abstract class HeroModel
{
    public int Experience { get; protected set; }
    public string Name { get; protected set; }
    public bool IsUnlocked { get; private set; }

    public event Action Unlocked;
    public event Action ExperienceUpdated;
    public event Action Unselected;

    public HeroModel(bool isAvailableAtStart)
    {
        IsUnlocked = isAvailableAtStart;
    }

    public void UnLock()
    {
        if (IsUnlocked == false)
        {
            IsUnlocked = true;
            Unlocked?.Invoke();
        }
    }

    public void AddExperience(int experience)
    {
        if (experience == 0)
            return;

        Experience += experience;
        ExperienceUpdated?.Invoke();
    }

    public void ToUnSelect()
    {
        Unselected?.Invoke();
    }
}