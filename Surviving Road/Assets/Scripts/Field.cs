using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public string Name, Type, Description, Image;
    public int Fuel, Space, DropRate;
    public float Damage, Protection, Energy, Health, Sickness;
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
    public int TimeCostA, TimeCostInvest;
    public bool Fight, ActionSearch, Intentions, Resolved;
    public string[] Dialogue, InvestigationDialogue;
    public int[] ActionInvestigates;
}
[Serializable]
public class Result
{
    public string Type, Name;
    public float Health;
    public int Energy;
    public bool FightWin, ExtraItemGain, ItemLoss;
    public string[] Text;
}
[Serializable]
public class Field
{
    public float health;
    public int energy;
    public float sickness;
    public int fuelStock;
    public Item equippedWeapon;
    public Item equippedProtection;
    public Road actualLocation;
    public Event actualEvent;
    public Item[] item;
    public string[] initialItem;
    public Road[] road;
    public Event[] events;
    public Result[] results;
}
