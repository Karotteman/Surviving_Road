using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public string Name, Type, Description, Image;
    public int Fuel, Health, Sickness, Energy, Space, Damage, Protection;
    public bool Consumable;
}
[Serializable]
public class Road
{
    public string Name, Hint, Description, Background;
    public Dictionary<string, int> LootRate;
}
[Serializable]
public class Field
{
    public Item[] item;
    public Road[] road;
}
