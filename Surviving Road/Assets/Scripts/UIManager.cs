using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject containerPanel;
    public GameObject sleepWarningPanel;
    public Text timeLeft;

    public InventoryManager inventoryManager;

    Transform containerStock;

    //public bool SetContainerDisplay { set { containerPanel.active; } }

    // Start is called before the first frame update
    void Start()
    {
        containerStock = containerPanel.transform.GetChild(3);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft.text = PlayerStats.health + " H";
    }

    public void DisplayContainer(string type)
    {
        Dictionary<Item, int> container = inventoryManager.GetContainer(type);

        if (sleepWarningPanel.activeSelf)
        {
            sleepWarningPanel.SetActive(false);
        }

        containerPanel.SetActive(true);
        CleanContainer();
        containerPanel.transform.GetChild(0).GetComponent<Text>().text = container.Keys.ToArray()[0].Type;

        
        int i = 0;
        foreach (KeyValuePair<Item, int> entry in container)
        {
            Image itemRenderer = containerStock.GetChild(i).GetChild(0).GetComponent<Image>();
            Sprite currentSprite = Resources.Load<Sprite>("Items/" + entry.Key.Image);

            itemRenderer.sprite = currentSprite;
            var tempColor = itemRenderer.color;
            tempColor.a = 1;
            itemRenderer.color = tempColor;

            itemRenderer.preserveAspect = true;
            itemRenderer.GetComponentInParent<ItemScript>().assignedItem = entry.Key;

            if (entry.Key.Consumable)
            {
                itemRenderer.transform.GetChild(0).GetComponent<Text>().text = entry.Value.ToString();
            }
            i++;
        }
    }

    public void DisplaySleepWarning()
    {
        if (containerPanel.activeSelf)
        {
            containerPanel.SetActive(false);
        }
        sleepWarningPanel.SetActive(!sleepWarningPanel.activeSelf);
    }

    public void CloseAllWindows()
    {
        if (sleepWarningPanel.activeSelf)
        {
            sleepWarningPanel.SetActive(false);
        }
        if (containerPanel.activeSelf)
        {
            containerPanel.SetActive(false);
        }
    }

    void CleanContainer()
    {
        for(int i = 0; i < containerStock.childCount; i++)
        {
            Image itemRenderer = containerStock.GetChild(i).GetChild(0).GetComponent<Image>();
            itemRenderer.sprite = null;
            var tempColor = itemRenderer.color;
            tempColor.a = 0;
            itemRenderer.color = tempColor;
            itemRenderer.transform.GetChild(0).GetComponent<Text>().text = "";
            if (itemRenderer.GetComponentInParent<ItemScript>().assignedItem != null)
            {
                itemRenderer.GetComponentInParent<ItemScript>().assignedItem = null;
            }
        }
    }
}
