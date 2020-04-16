using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    //List<Road> roads;
    int nbRoadMin = 2;
    int nbRoadMax = 4;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerStats.actulLocation == null)
        {
            GenerateRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRoad()
    {
        PlayerStats.locationOptions = new List<Road>();
        for (int i = 0; i < Random.Range(nbRoadMin, nbRoadMin); i++)
        {
            PlayerStats.locationOptions.Add(PlayerStats.roads[Random.Range(0, PlayerStats.roads.Length)]);
        }

    }
}
