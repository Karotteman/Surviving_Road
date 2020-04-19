using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Inventory
{

    public static int fuelStock;
    public static Dictionary<Item, int> foodStock;
    public static Dictionary<Item, int> WaterStock;
    public static Dictionary<Item, int> medpackStock;
    public static Dictionary<Item, int> antibioticStock;
    public static Dictionary<Item, int> weaponStock;
    public static Dictionary<Item, int> protectionStock;

    static int stackLimit = 10;
    static string currentTypeContainer;
    static string[] containerType = { "Medpack", "Antibiotic", "Protection", "Weapon", "Water", "Food" };

    //public static void UseItem()
    //{
    //    foreach(Item item in Player.item)
    //    {
    //        //if (item.Name == itemName)
    //        //{
    //        //    //var t
    //        //}
    //    }
    //}

    public static Dictionary<Item, int> LoadContainer(string type)
    {
        Dictionary<Item, int> currentContainer = GetContainer(type);
        if (currentContainer != null)
        {
            return currentContainer;
        }
        else
        {
            if(type == "Next")
            {
                NextContainer();
                return LoadContainer(currentTypeContainer);
            }
            else if (type == "Previous")
            {
                PreviousContainer();
                return LoadContainer(currentTypeContainer);
            }
        }
        return null;
    }

    public static Dictionary<Item, int> GetContainer(string type)
    {
        switch (type)
        {
            case "Food":
                currentTypeContainer = type;
                return foodStock;
            case "Water":
                currentTypeContainer = type;
                return WaterStock;
            case "Medpack":
                currentTypeContainer = type;
                return medpackStock;
            case "Antibiotic":
                currentTypeContainer = type;
                return antibioticStock;
            case "Weapon":
                currentTypeContainer = type;
                return weaponStock;
            case "Protection":
                currentTypeContainer = type;
                return protectionStock;
            case "All":
                Dictionary<Item, int> tempContainer = new Dictionary<Item, int>();
                foreach (string tempType in containerType)
                {
                    foreach(KeyValuePair<Item, int> entry in GetContainer(tempType))
                    {
                        tempContainer.Add(entry.Key, entry.Value);
                    }
                }
                return tempContainer;
            default:
                return null;
        }
    }


    public static void Pickup(Item item)
    {
        if (item.Type == "Fuel")
        {
            fuelStock += item.Fuel;
        }
        else if (GetContainer(item.Type).ContainsKey(item) && item.Consumable)
        {
            if (GetContainer(item.Type)[item] < stackLimit)
            {
                GetContainer(item.Type)[item]++;
            }
            else
            {
                //GetContainer(item.Type).Add(item, 1);
            }
        }
        else
        {
            if (GetContainer(item.Type).ContainsKey(item))
            {
                //GetContainer(item.Type).Add(item, GetContainer(item.Type)[item]++);
            }
            else
            {
                GetContainer(item.Type).Add(item, 1);
            }
        }
    }
    public static void Remove(Item item)
    {
        if (item.Type != "Fuel")
        {
            if (GetContainer(item.Type).ContainsKey(item) && item.Consumable)
            {
                if (GetContainer(item.Type)[item] > 1)
                {
                    GetContainer(item.Type)[item]--;
                }
                else
                {
                    GetContainer(item.Type).Remove(item);
                }
            }
            else
            {
                GetContainer(item.Type).Remove(item);
            }
        }
    }

    public static void NextContainer()
    {
        for (int i = 0; i < containerType.Length; i++)
        {
            if (containerType[i] == currentTypeContainer)
            {
                if(i == containerType.Length-1)
                {
                    currentTypeContainer = containerType[0];
                }
                else
                {
                    currentTypeContainer = containerType[i+1];
                }
                return;
            }
        }
    }

    public static void PreviousContainer()
    {
        for (int i = 0; i < containerType.Length; i++)
        {
            if (containerType[i] == currentTypeContainer)
            {
                if (i == 0)
                {
                    currentTypeContainer = containerType[containerType.Length-1];
                }
                else
                {
                    currentTypeContainer = containerType[i-1];
                }
                return;
            }
        }
    }

    public static void CleanContainer(Dictionary<Item, int> container)
    {
        foreach(KeyValuePair<Item, int> entry in container)
        {
            if(entry.Value == 0)
            {
                container.Remove(entry.Key);
            }
        }
    }
}
