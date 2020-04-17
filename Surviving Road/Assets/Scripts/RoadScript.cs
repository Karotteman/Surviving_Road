using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public UIManager uiManager;
    Road assignedRoad = null;

    int fuelUse;
    int timeUse;

    public void AddRoad(Road tempRoad)
    {
        assignedRoad = tempRoad;
        fuelUse = Random.Range(assignedRoad.FuelMin, assignedRoad.FuelMax);
        timeUse = Random.Range(assignedRoad.TimeMin, assignedRoad.TimeMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseOver()
    {
        print(assignedRoad.Name);
        print(assignedRoad.FuelMin + " ||| "+ assignedRoad.FuelMax + " ||| "+ assignedRoad.TimeMin + " ||| " + assignedRoad.TimeMax);
        if (assignedRoad != null)
        {
            uiManager.DisplayDescritpion(
                assignedRoad.Description,
                fuelUse,
                timeUse,
                GetComponentInParent<RectTransform>().transform.position,
                GetComponentInParent<RectTransform>().transform.localScale.x
                );
        }
    }
    public void MouseExit()
    {
        uiManager.DisplayDescritpion();
    }

    public void OnClick()
    {
        PlayerStats.actualEvent = PlayerStats.events[Random.Range(0, PlayerStats.events.Length)];
    }
}
