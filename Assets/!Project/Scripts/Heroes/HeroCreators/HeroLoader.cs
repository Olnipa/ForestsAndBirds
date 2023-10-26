using System;
using System.Collections.Generic;

public class HeroLoader
{
    public Dictionary<Type, HeroModel> GetHeroes()
    {
        return new Dictionary<Type, HeroModel>()
        {
            { typeof(Hawk), new Hawk() },
            { typeof(Gull), new Gull() },
            { typeof(Crow), new Crow() },
            { typeof(Owl), new Owl() },
        };
    }
}