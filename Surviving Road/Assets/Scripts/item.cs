using System;

[Serializable]
public class Item
{
    //public static Item[] list;

    public string Name, Type, Description, Image;
    public int Fuel, Health, Sickness, Energy, Space, Damage, Protection;
    public bool Consumable;

    //public string Name { get { return name; } }
    //public int Health { get { return health; } }
    //public int Sickness { get { return sickness; } }
    //public int Energy { get { return energy; } }
    //public int Space { get { return space; } }

    //public void Instantiate(string nam, int heal, int sick, int eng, int spa)
    //{
    //    name = nam;
    //    health = heal;
    //    sickness = sick;
    //    energy = eng;
    //    space = spa;
    //}
}
[Serializable]
public class Ressources
{
    public Item[] item;
}
