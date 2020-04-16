using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public UIManager uiManager;
    public Item assignedItem = null;
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
        if (assignedItem != null)
        {
            uiManager.DisplayItemDescritpion(
                assignedItem.Name,
                assignedItem.Description,
                GetComponentInParent<RectTransform>().transform.position,
                GetComponentInParent<RectTransform>().transform.localScale.x
                );
        }
    }
    public void MouseExit()
    {
        uiManager.DisplayItemDescritpion();
    }

    public void OnClick()
    {
        if (assignedItem != null)
        {
            print("click");
        }
    }
}
