﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class JsonManager : MonoBehaviour
{
    //Ressources ressources;
    InventoryManager inventoryManager;

    // Use this for initialization
    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
        //Field res = JsonUtility.FromJson<Field>(File.ReadAllText("./Assets/Resources/Jsons/ressources.json"));
        Field res = JsonUtility.FromJson<Field>(Resources.Load<TextAsset>("Jsons/ressources").ToString());
        PlayerStats.item = res.item;
        PlayerStats.roads = res.road;
        PlayerStats.events = res.events;
        PlayerStats.results = res.results;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewSave()
    {
        //Field res = JsonUtility.FromJson<Field>(File.ReadAllText("./Assets/Resources/Jsons/initialState.json"));
        Field res = JsonUtility.FromJson<Field>(Resources.Load<TextAsset>("Jsons/initialState").ToString());
        foreach (Item item in res.item)
        {
            inventoryManager.Pickup(item);
        }
        PlayerStats.health = res.health;
        PlayerStats.energy = res.energy;
        PlayerStats.sickness = res.sickness;
    }
}
