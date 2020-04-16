using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    Dictionary<Item, int> currentContainer;
    int stackLimit = 10;
    string currentTypeContainer;
    string[] containerType = { "Medpack", "Antibiotic", "Protection", "Weapon", "Food", "Water" };

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

    public Dictionary<Item, int> GetContainer(string type)
    {
        switch (type)
        {
            case "Food":
                currentContainer = PlayerStats.foodStock;
                currentTypeContainer = type;
                break;
            case "Water":
                currentContainer = PlayerStats.WaterStock;
                currentTypeContainer = type;
                break;
            case "Medpack":
                currentContainer = PlayerStats.medpackStock;
                currentTypeContainer = type;
                break;
            case "Antibiotic":
                currentContainer = PlayerStats.antibioticStock;
                currentTypeContainer = type;
                break;
            case "Weapon":
                currentContainer = PlayerStats.weaponStock;
                currentTypeContainer = type;
                break;
            case "Protection":
                currentContainer = PlayerStats.protectionStock;
                currentTypeContainer = type;
                break;
            case "Next":
                NextContainer();
                GetContainer(currentTypeContainer);
                break;
            case "Previous":
                PreviousContainer();
                GetContainer(currentTypeContainer);
                break;
        }

        return currentContainer;
    }

    public void Pickup(Item item)
    {
        Dictionary<Item, int> currentContainer;

        switch (item.Type)
        {
            case "Food":
                currentContainer = PlayerStats.foodStock;
                break;
            case "Water":
                currentContainer = PlayerStats.WaterStock;
                break;
            case "Medpack":
                currentContainer = PlayerStats.medpackStock;
                break;
            case "Antibiotic":
                currentContainer = PlayerStats.antibioticStock;
                break;
            case "Weapon":
                currentContainer = PlayerStats.weaponStock;
                break;
            case "Protection":
                currentContainer = PlayerStats.protectionStock;
                break;
            default:
                currentContainer = PlayerStats.foodStock;
                break;
        }

        if (item.Type == "Fuel")
        {
            PlayerStats.fuelStock += item.Fuel;
        }
        else if (currentContainer.ContainsKey(item) && item.Consumable)
        {
            if (currentContainer[item] < stackLimit)
            {
                currentContainer[item]++;
            }
            else
            {
                currentContainer.Add(item, 1);
            }
        }
        else
        {
            currentContainer.Add(item, 1);
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
