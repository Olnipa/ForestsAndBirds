public class Hawk : HeroModel
{
    private const string _defaultHawkName = "������";

    public Hawk(bool isAvailableAtStart = true) : base(isAvailableAtStart)
    {
        Name = _defaultHawkName;
    }
}