public class Owl : HeroModel
{
    private const string _defaultOwlName = "�����";

    public Owl(bool isAvailableAtStart = false) : base(isAvailableAtStart)
    {
        Name = _defaultOwlName;
    }
}