﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager gameManager;
    InventoryManager inventoryManager;
    int sleepTimeMin = 1;
    int sleepTimeMax = 12;
    float regen = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StillAlive()
    {
        if (PlayerStats.health <= 0 || PlayerStats.energy <= 0)
        {
            gameManager.GameOver();
        }
        else
        {
            if (PlayerStats.energy <= 10 || PlayerStats.health <= 0.25)
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().SwitchMusic("battle");
            }
            else if (PlayerStats.energy > 10 && PlayerStats.health > 0.25)
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().SwitchMusic("main");
            }
        }
    }

    public void Sleep()
    {
        int sleepTime = Random.Range(sleepTimeMin, sleepTimeMax);
        TimeSpent(sleepTime, true);
        if(PlayerStats.inGame) gameManager.LoadScene(1);
    }

    public void TimeSpent(int time, [Optional] bool sleeping)
    {
        if (sleeping)
        {
            PlayerStats.energy -= (int)Mathf.Round(time * PlayerStats.sickness);
        }
        else
        {
            PlayerStats.energy -= (int)Mathf.Round(time * PlayerStats.sickness + (time - PlayerStats.health * time));
        }
        if (PlayerStats.health < 1)
        {
            SetHealth( time * regen, false);
        }
        StillAlive();
    }

    public void UseItem(Item tempItem)
    {
        if (tempItem.Consumable)
        {
            //print(tempItem.Health+ "H & E"+ tempItem.Energy);
            PlayerStats.energy += tempItem.Energy;
            SetHealth( tempItem.Health);
            SetSickness(tempItem.Sickness);
            inventoryManager.Remove(tempItem);
        }
        else
        {
            switch (tempItem.Type)
            {
                case "Weapon":
                    PlayerStats.equippedWeapon = tempItem;
                    break;
                case "Protection":
                    PlayerStats.equippedProtection = tempItem;
                    break;
            }
        }
        StillAlive();
    }

    public void SetHealth(float health, [Optional] bool checkState)
    {
        PlayerStats.health += health;
        if (PlayerStats.health > 1)
        {
            PlayerStats.health = 1;
        }
        else if (PlayerStats.health < 0)
        {
            PlayerStats.health = 0;
        }
        if(checkState)StillAlive();
    }
    public void SetSickness(float sickness)
    {
        PlayerStats.sickness += sickness;
        if (PlayerStats.sickness < 0.1)
        {
            PlayerStats.sickness = 0.1f;
        }
    }
}
