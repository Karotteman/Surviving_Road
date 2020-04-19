using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Stack the state of the player during the game
/// </summary>
public static class Player
{
    public static bool inGame;
    public static Item[] item;
    public static Road[] roads;
    public static Event[] events;
    public static Result[] results;

    static float health;
    static float energyMax;
    static int energy;
    static float sickness;
    static float regen = 0.01f;
    static float sleepRegen = regen * 2;

    public static Item equippedWeapon;
    public static Item equippedProtection;

    public static Road actualLocation;
    public static Dictionary<Road, int[]> locationOptions;
    public static Event actualEvent;

    /// <summary>
    /// Return the value of enery left with sickness
    /// </summary>
    static float EnergyMax
    {
        get { return energyMax / Sickness; }
        set { energyMax = value * Sickness; }
    }

    /// <summary>
    /// Return the value of enery left with sickness and health
    /// </summary>
    public static float Energy
    {
        get 
        {
            int tempEnergy = (int)Mathf.Round(EnergyMax * Health);
            if (tempEnergy < 0) tempEnergy = 0;
            return tempEnergy; 
        }
        set { EnergyMax = value / Health; }
    }

    /// <summary>
    /// Restrain the health value
    /// </summary>
    public static float Health
    {
        set
        {
            health = value;
            if (health > 1)
            {
                health = 1;
            }
            else if (health < 0)
            {
                health = 0;
            }
        }
        get { return health; }
    }

    /// <summary>
    /// Restrain the sickness value
    /// </summary>
    public static float Sickness
    {
        get{ return sickness; }
        set
        {
            sickness = value;
            if (sickness < 0.1)
            {
                sickness = 0.1f;
            }
        }
    }

    /// <summary>
    /// Calculate the effect of time
    /// </summary>
    /// <param name="time">The time pasted</param>
    /// <param name="sleeping">If the player sleeping</param>
    public static void TimeSpent(int time, [Optional] bool sleeping)
    {
        if (sleeping)
        {
            float tempHealth = Health;
            Health += time * sleepRegen;
            EnergyMax -= (int)Mathf.Round(time / (tempHealth+ Health)/2);
        }
        else
        {
            EnergyMax -= (int)Mathf.Round(time/health);
            Health += time * regen;
        }
    }

    public static Item[] GetItemList(string type)
    {
        List<Item> tempList = new List<Item>();
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].Type == type)
            {
                tempList.Add(item[i]);
            }
        }
        return tempList.ToArray();
    }

    /// <summary>
    /// Return a Item by Name
    /// </summary>
    /// <param name="name">Name of the item</param>
    /// <returns></returns>
    public static Item GetItem(string name)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].Name == name)
            {
                return item[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Get a Random Item of the specifie type
    /// </summary>
    /// <param name="type">Type of the item</param>
    /// <returns></returns>
    public static Item GetRandomItem(string type)
    {
        List<Item> tempList = new List<Item>();
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].Type == type)
            {
                tempList.Add(item[i]);
            }
        }
        return tempList[Random.Range(0, tempList.Count)];
    }

    /// <summary>
    /// Get a Random Item of the specifie type (the last item in range not inculde)
    /// </summary>
    /// <param name="type">Type of the item</param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static Item GetRandomItem(string type, int range)
    {
        List<Item> tempList = new List<Item>();
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].Type == type)
            {
                tempList.Add(item[i]);
            }
        }
        return tempList[Random.Range(0, tempList.Count - range)];
    }
}
