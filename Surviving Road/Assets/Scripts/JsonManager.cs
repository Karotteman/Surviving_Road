using UnityEngine;
using System.Collections;
using System.IO;

public class JsonManager : MonoBehaviour
{
    //Ressources ressources;
    InventoryManager inventoryManager;

    // Use this for initialization
    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
        Field res = JsonUtility.FromJson<Field>(File.ReadAllText("./Assets/Resources/Jsons/ressources.json"));
        PlayerStats.item = res.item;
        PlayerStats.roads = res.road;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewSave()
    {
        Field res = JsonUtility.FromJson<Field>(File.ReadAllText("./Assets/Resources/Jsons/initialState.json"));
        foreach(Item item in res.item)
        {
            inventoryManager.Pickup(item);
        }
        PlayerStats.health = res.health;
        PlayerStats.sickness = res.sickness;
    }
}
