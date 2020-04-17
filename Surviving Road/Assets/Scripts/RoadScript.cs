using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public UIManager uiManager;
    public RoadManager roadManager;
    Road assignedRoad = null;


    public void AddRoad(Road tempRoad)
    {
        assignedRoad = tempRoad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseOver()
    {
        if (assignedRoad != null)
        {
            int fuelCost = PlayerStats.locationOptions[assignedRoad][0];
            int timeCost = PlayerStats.locationOptions[assignedRoad][1];
            uiManager.DisplayDescritpion(
                assignedRoad.Description,
                fuelCost,
                (int)Mathf.Round(timeCost * PlayerStats.sickness + (timeCost - PlayerStats.health * timeCost)),
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
        if (assignedRoad != null)
        {
            PlayerStats.actualEvent = PlayerStats.events[Random.Range(0, PlayerStats.events.Length)];
            roadManager.UseRoad(PlayerStats.locationOptions[assignedRoad][0], PlayerStats.locationOptions[assignedRoad][1]);
        }
    }
}
