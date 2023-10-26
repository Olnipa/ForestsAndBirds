public class Hawk : HeroModel
{
    private const string _defaultHawkName = "ястреб";

    public Hawk(bool isAvailableAtStart = true) : base(isAvailableAtStart)
    {
        Name = _defaultHawkName;
    }
}