using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    PlayerManager playerManager;
    public GameManager gameManager;
    //List<Road> roads;
    int nbRoadMin = 2;
    int nbRoadMax = 4;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        if(Player.locationOptions == null)
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
        Inventory.fuelStock -= fuel;
        Player.TimeSpent(time);
        playerManager.StillAlive();
        GenerateRoad();
        if (Player.inGame) gameManager.LoadScene(3);
    }

    public void GenerateRoad()
    {
        Player.locationOptions = new Dictionary<Road, int[]>();
        for (int i = 0; i < Random.Range(nbRoadMin, nbRoadMax+1); i++)
        {
            AddRoadToOptions();
        }

    }

    void AddRoadToOptions()
    {
        Road roadTemp = Player.roads[Random.Range(0, Player.roads.Length)];
        int fuelUse = Random.Range(roadTemp.FuelMin, roadTemp.FuelMax + 1);
        int timeUse = Random.Range(roadTemp.TimeMin, roadTemp.TimeMax + 1);
        int[] Costs = { fuelUse, timeUse };
        if (Player.locationOptions.ContainsKey(roadTemp))
        {
            AddRoadToOptions();
            return;
        }
        Player.locationOptions.Add(roadTemp, Costs);
    }
}
