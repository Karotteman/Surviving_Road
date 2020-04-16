using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    public UIManager uiManager;
    public Road assignedRoad = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseOver()
    {
        if (assignedRoad != null)
        {
            uiManager.DisplayDescritpion(
                assignedRoad.Description,
                Random.Range(assignedRoad.FuelMin, assignedRoad.FuelMax),
                Random.Range(assignedRoad.TimeMin, assignedRoad.TimeMax),
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
            print("click");
        }
    }
}
