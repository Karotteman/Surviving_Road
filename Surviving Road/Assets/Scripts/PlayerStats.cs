using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int health;
    public static int sickness;
    public static int fuelStock;
    public static Item equippedWeapon;
    public static Item equippedProtection;
    public static Item[] item;
    public static Road[] roads;
    public static Dictionary<Item, int> foodStock;
    public static Dictionary<Item, int> WaterStock;
    public static Dictionary<Item, int> medpackStock;
    public static Dictionary<Item, int> antibioticStock;
    public static Dictionary<Item, int> weaponStock;
    public static Dictionary<Item, int> protectionStock;
}
