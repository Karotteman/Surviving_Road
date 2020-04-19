using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class JsonManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Field res = JsonUtility.FromJson<Field>(Resources.Load<TextAsset>("Jsons/ressources").ToString());
        Player.item = res.item;
        Player.roads = res.road;
        Player.events = res.events;
        Player.results = res.results;
    }

    public void NewSave()
    {
        Field res = JsonUtility.FromJson<Field>(Resources.Load<TextAsset>("Jsons/initialState").ToString());
        foreach (string itemName in res.initialItem)
        {
            Inventory.Pickup(Player.GetItem(itemName));
        }
        Player.Health = res.health;
        Player.Sickness = res.sickness;
        Player.Energy = res.energy;
        Inventory.fuelStock = res.fuelStock;
        Player.equippedWeapon = null;
        Player.equippedProtection = null;
        Player.actualLocation = res.actualLocation;
        Player.actualEvent = res.actualEvent;
    }
}
