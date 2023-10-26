public class Gull : HeroModel
{
    private const string _defaultGullName = "Чайка";

    public Gull(bool isAvailableAtStart = false) : base(isAvailableAtStart)
    {
        Name = _defaultGullName;
    }
}