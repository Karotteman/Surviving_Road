using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameManager gameManager;
    int sleepTimeMin = 1;
    int sleepTimeMax = 12;
    float regen = 0.05f;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Throw game over if is not
    /// </summary>
    public void StillAlive()
    {
        if (Player.Health <= 0 || Player.Energy <= 0)
        {
            gameManager.GameOver();
        }
        else
        {
            if (Player.Energy <= 10 || Player.Health <= 0.25)
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().SwitchMusic("battle");
            }
            else if (Player.Energy > 10 && Player.Health > 0.25)
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().SwitchMusic("main");
            }
        }
    }

    public void Sleep()
    {
        int sleepTime = UnityEngine.Random.Range(sleepTimeMin, sleepTimeMax);
        Player.TimeSpent(sleepTime, true);
        StillAlive();
        if(Player.inGame) gameManager.LoadScene(1);
    }

    public void UseItem(Item tempItem)
    {
        if (tempItem.Consumable)
        {
            Player.Energy += tempItem.Energy;
            Player.Health += tempItem.Health;
            Player.Sickness += tempItem.Sickness;
            Inventory.Remove(tempItem);
        }
        else
        {
            switch (tempItem.Type)
            {
                case "Weapon":
                    Player.equippedWeapon = tempItem;
                    break;
                case "Protection":
                    Player.equippedProtection = tempItem;
                    break;
            }
        }
        StillAlive();
    }
}
