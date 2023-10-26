public class Crow : HeroModel
{
    private const string _defaultCrowName = "Ворон";

    public Crow(bool isAvailableAtStart = false) : base(isAvailableAtStart)
    {
        Name = _defaultCrowName;
    }
}