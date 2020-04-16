using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public string Name, Type, Description, Image;
    public int Fuel, Health, Sickness, Energy, Space, Damage, Protection;
    public bool Consumable;

    public override int GetHashCode()
    {
        if (Name == null) return 0;
        return Name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        Item other = obj as Item;
        return other != null && other.Name == this.Name;
    }
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
    public int health;
    public float sickness;
    public Item[] item;
    public Road[] road;
}
