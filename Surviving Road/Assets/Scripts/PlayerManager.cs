using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int sleepTimeMin = 1;
    int sleepTimeMax = 12;
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

        TimeSpent(sleepTime);
    }

    public void TimeSpent(float time)
    {
        PlayerStats.energy -= time * PlayerStats.sickness + time - PlayerStats.health;
    }
}
