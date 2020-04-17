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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sleep()
    {
        int sleepTime = Random.Range(sleepTimeMin, sleepTimeMax);
        TimeSpent(sleepTime, true);
        gameManager.LoadScene("HomeScene");
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
            PlayerStats.health += time * regen;
            if (PlayerStats.health > 1)
            {
                PlayerStats.health = 1;
            }
        }
    }
}
