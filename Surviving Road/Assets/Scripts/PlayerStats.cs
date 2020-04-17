using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static bool inGame;
    public static Item[] item;
    public static Road[] roads;
    public static Event[] events;
    public static Result[] results;

    public static float health;
    public static int energy;
    public static float sickness;

    public static Item equippedWeapon;
    public static Item equippedProtection;
    public static int fuelStock;
    public static Dictionary<Item, int> foodStock;
    public static Dictionary<Item, int> WaterStock;
    public static Dictionary<Item, int> medpackStock;
    public static Dictionary<Item, int> antibioticStock;
    public static Dictionary<Item, int> weaponStock;
    public static Dictionary<Item, int> protectionStock;

    public static Road actualLocation;
    public static Dictionary<Road, int[]> locationOptions;
    public static Event actualEvent;

    public static Item[] GetItemList(string type) 
    {
        List<Item> tempList = new List<Item>();
        for (int i = 0; i < item.Length; i++)
        {
            if(item[i].Type == type)
            {
                tempList.Add(item[i]);
            }
        }
        return tempList.ToArray();  
    }
}
