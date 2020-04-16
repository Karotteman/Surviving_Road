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
    public string Name, Description, Background;
    public int TimeMin, TimeMax, FuelMin, FuelMax;
    public LootRate[] LootRate;
}
[Serializable]
public class LootRate
{
    public string Loot;
    public int DropRate;
}
[Serializable]
public class Event
{
    public string Type, actionAccept, actionRefused, actionLeave;
    public int TimeCost, InvestigateTimeCost;
    public bool Fight, ActionSearch, Intentions;
    public string[] Dialogues;
    public int[] ActionInvestigates;
}
[Serializable]
public class Result
{
    public string Type, Text;
    public int Health, Energy;
    public bool FightWin, ExtraItemGain, ItemLoss;
}
[Serializable]
public class Field
{
    public int health;
    public float sickness;
    public Item[] item;
    public Road[] road;
    public Event[] events;
    public Result[] results;
}
