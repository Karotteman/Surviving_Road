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

    public static int health;
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

    public static Road location;
    public static List<Road> locationOptions;
    public static Event actualEvent;
}
