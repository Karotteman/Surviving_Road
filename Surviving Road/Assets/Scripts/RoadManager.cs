﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    PlayerManager playerManager;
    //List<Road> roads;
    int nbRoadMin = 2;
    int nbRoadMax = 4;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        if(PlayerStats.locationOptions == null)
        {
            GenerateRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseRoad(int fuel, int time)
    {
        PlayerStats.fuelStock -= fuel;
        playerManager.TimeSpent(time);
        GenerateRoad();
    }

    public void GenerateRoad()
    {
        PlayerStats.locationOptions = new Dictionary<Road, int[]>();
        for (int i = 0; i < Random.Range(nbRoadMin, nbRoadMax); i++)
        {
            Road roadTemp = PlayerStats.roads[Random.Range(0, PlayerStats.roads.Length)];
            int fuelUse = Random.Range(roadTemp.FuelMin, roadTemp.FuelMax);
            int timeUse = Random.Range(roadTemp.TimeMin, roadTemp.TimeMax);
            int[] Costs = { fuelUse, timeUse };
            PlayerStats.locationOptions.Add(roadTemp, Costs);
        }

    }
}
