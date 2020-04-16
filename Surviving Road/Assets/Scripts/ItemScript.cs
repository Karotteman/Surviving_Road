using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
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
        if(assignedItem != null)
        {
            print(assignedItem.Name);
        }
    }

    public void OnClick()
    {
        if (assignedItem != null)
        {
            print("click");
        }
    }
}
