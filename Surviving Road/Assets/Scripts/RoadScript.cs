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
            int fuelCost = Player.locationOptions[assignedRoad][0];
            int timeCost = Player.locationOptions[assignedRoad][1];
            uiManager.DisplayDescritpion(
                assignedRoad.Description,
                fuelCost,
                timeCost,
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
            Player.actualLocation = assignedRoad;
            Player.actualEvent = Player.events[Random.Range(0, Player.events.Length)];
            //Player.actualEvent = Player.events[8];
            roadManager.UseRoad(Player.locationOptions[assignedRoad][0], Player.locationOptions[assignedRoad][1]);
        }
    }
}
