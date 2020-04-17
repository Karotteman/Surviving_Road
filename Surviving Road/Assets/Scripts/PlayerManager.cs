using System.Collections;
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

    public bool StillAlive()
    {
        if (PlayerStats.health <= 0 || PlayerStats.energy <= 0)
        {
            return false;
        }
        return true;
    }

    public void Sleep()
    {
        int sleepTime = Random.Range(sleepTimeMin, sleepTimeMax);
        TimeSpent(sleepTime, true);
        if (StillAlive())
        {
            gameManager.LoadScene("HomeScene");
        }
        else
        {
            gameManager.GameOver();
        }
    }

    public void TimeSpent(float time, [Optional] bool sleeping)
    {
        if (sleeping)
        {
            PlayerStats.energy -= time * PlayerStats.sickness;
        }
        else
        {
            PlayerStats.energy -= time * PlayerStats.sickness + (time - PlayerStats.health * time);
        }
        if (PlayerStats.health < 1)
        {
            SetHealth( time * regen);
        }
    }

    public void UseItem(Item tempItem)
    {
        if (tempItem.Consumable)
        {
            PlayerStats.health += tempItem.Health;
            SetHealth( tempItem.Energy);
            PlayerStats.sickness += tempItem.Sickness;
            inventoryManager.Remove(tempItem);
        }
    }

    void SetHealth(float health)
    {
        PlayerStats.health += health;
        if (PlayerStats.health > 1)
        {
            PlayerStats.health = 1;
        }
        else if(PlayerStats.health < 0)
        {
            PlayerStats.health = 0;
        }
    }
}
