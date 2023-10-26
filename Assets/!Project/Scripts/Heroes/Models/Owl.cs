public class Owl : HeroModel
{
    private const string _defaultOwlName = "Совух";

    public Owl(bool isAvailableAtStart = false) : base(isAvailableAtStart)
    {
        Name = _defaultOwlName;
    }
}