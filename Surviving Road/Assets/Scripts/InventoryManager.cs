using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    //Dictionary<Item, int> currentContainer;
    int stackLimit = 10;
    string currentTypeContainer;
    string[] containerType = { "Medpack", "Antibiotic", "Protection", "Weapon", "Water", "Food" };

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItem()
    {
        foreach(Item item in PlayerStats.item)
        {
            //if (item.Name == itemName)
            //{
            //    //var t
            //}
        }
    }

    public Dictionary<Item, int> LoadContainer(string type)
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

    public Dictionary<Item, int> GetContainer(string type)
    {
        switch (type)
        {
            case "Food":
                currentTypeContainer = type;
                return PlayerStats.foodStock;
            case "Water":
                currentTypeContainer = type;
                return PlayerStats.WaterStock;
            case "Medpack":
                currentTypeContainer = type;
                return PlayerStats.medpackStock;
            case "Antibiotic":
                currentTypeContainer = type;
                return PlayerStats.antibioticStock;
            case "Weapon":
                currentTypeContainer = type;
                return PlayerStats.weaponStock;
            case "Protection":
                currentTypeContainer = type;
                return PlayerStats.protectionStock;
            default :
                return null;
        }
    }

    public void Pickup(Item item)
    {        
        if (item.Type == "Fuel")
        {
            PlayerStats.fuelStock += item.Fuel;
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
    public void Remove(Item item)
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

    public void NextContainer()
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

    public void PreviousContainer()
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

    public void CleanContainer(Dictionary<Item, int> container)
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
