using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject containerPanel;
    public GameObject sleepWarningPanel;
    public Transform itemDescriptionPanel;
    public Text timeLeft;
    public Transform roads;
    public InventoryManager inventoryManager;


    [Header("Events UI")]
    public Text dialogues;
    public SpriteRenderer background;
    public EventManager eventManager;
    public Transform buttonHolder;
    public bool GotDialogue = false;

    Transform containerStock;

    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().name) //Le switch vérifie que t'es dans la bonne scene
        {
            case "HomeScene":
                containerStock = containerPanel.transform.GetChild(3);
                break;
            case "RoadScene":
                DisplayRoads();
                break;
            case "EventScene":
                print(PlayerStats.actualLocation.Background);
                Sprite currentSprite = Resources.Load<Sprite>("Images/Backgrounds/" + PlayerStats.actualLocation.Background);
                background.sprite = currentSprite;
                DisplayDialogues(); //Fonction pour afficher les dialogues
                DisplayButtons(); //Fonction pour afficher les boutons
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft.text = PlayerStats.energy + "H  "+ (int)Mathf.Round(PlayerStats.health*100)+"Life";
    }

    /////////////////////////// DOMINIC ///////////////////////////////////
    void DisplayDialogues()
    {
        if (!GotDialogue)
        {
            dialogues.text = PlayerStats.actualEvent.Dialogue[Random.Range(0, PlayerStats.actualEvent.Dialogue.Length)];
            GotDialogue = true;
        }

    }

    public void DisplayButtons()
    {
        //Active le bouton search si actionSearch est vrai
        buttonHolder.GetChild(0).gameObject.SetActive(PlayerStats.actualEvent.ActionSearch);

        if (PlayerStats.actualEvent.Type == "None" || PlayerStats.actualEvent.Type == "Fight")
        {
            buttonHolder.GetChild(1).gameObject.SetActive(false);
            buttonHolder.GetChild(2).gameObject.SetActive(false);
            buttonHolder.GetChild(3).gameObject.SetActive(false);
        }
        
        buttonHolder.GetChild(4).gameObject.SetActive(!PlayerStats.actualEvent.Fight);
        buttonHolder.GetChild(5).gameObject.SetActive(PlayerStats.actualEvent.Fight);
        
    }
    /////////////////////////// DOMINIC ///////////////////////////////////

    void DisplayRoads()
    {
        CleanRoads();
        int i = 0;
        foreach (KeyValuePair<Road,int[]> option in PlayerStats.locationOptions)
        {
            Transform tempRoad = roads.GetChild(i);

            tempRoad.GetComponent<RoadScript>().AddRoad(option.Key);
            tempRoad.gameObject.SetActive(true);
            tempRoad.GetChild(0).GetComponent<Text>().text = option.Key.Name;

            if(option.Value[0] > PlayerStats.fuelStock)
            {
                tempRoad.GetComponent<Button>().interactable = false;
            }

            i++;
        }
    }

    void CleanRoads()
    {
        for(int i = 0; i < roads.childCount;i++)
        {
            roads.GetChild(i).gameObject.SetActive(false);
            roads.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void DisplayDescritpion(string name, string description, Vector2 position, float sizeButton)
    {
        itemDescriptionPanel.gameObject.SetActive(true);
        itemDescriptionPanel.position = position + new Vector2(2 * sizeButton / 3, sizeButton / 2);
        itemDescriptionPanel.GetChild(0).GetComponent<Text>().text = name;
        itemDescriptionPanel.GetChild(2).GetComponent<Text>().text = description;
    }
    public void DisplayDescritpion(string description, int fuel, int time, Vector2 position, float sizeButton)
    {
        itemDescriptionPanel.gameObject.SetActive(true);
        itemDescriptionPanel.position = position + new Vector2(4 * sizeButton / 3, sizeButton / 2);
        itemDescriptionPanel.GetChild(0).GetComponent<Text>().text = "-"+ fuel +" Fuel  -"+time+" Hour";
        itemDescriptionPanel.GetChild(2).GetComponent<Text>().text = description;
    }
    public void DisplayDescritpion()
    {
        itemDescriptionPanel.gameObject.SetActive(false);
    }

    public void RefreshContainer(Dictionary<Item, int> container)
    {
        CleanContainer();
        int i = 0;

        foreach (KeyValuePair<Item, int> entry in container)
        {
            Image itemRenderer = containerStock.GetChild(i).GetChild(0).GetComponent<Image>();
            Sprite currentSprite = Resources.Load<Sprite>("Images/Items/" + entry.Key.Image);

            itemRenderer.sprite = currentSprite;
            var tempColor = itemRenderer.color;
            tempColor.a = 1;
            itemRenderer.color = tempColor;
            itemRenderer.preserveAspect = true;

            itemRenderer.GetComponentInParent<ItemScript>().assignedItem = entry.Key;
            itemRenderer.GetComponentInParent<Button>().interactable = true;
            if (entry.Key.Consumable)
            {
                itemRenderer.transform.GetChild(0).GetComponent<Text>().text = entry.Value.ToString();
            }
            else if(entry.Key.Equals(PlayerStats.equippedProtection) || entry.Key.Equals(PlayerStats.equippedWeapon))
            {
                itemRenderer.transform.GetChild(0).GetComponent<Text>().text = "E";
            }
            i++;
        }
    }
    public void DisplayContainer(string type)
    {
        Dictionary<Item, int> container = inventoryManager.LoadContainer(type);

        if (sleepWarningPanel.activeSelf)
        {
            sleepWarningPanel.SetActive(false);
        }

        containerPanel.SetActive(true);
        containerPanel.transform.GetChild(0).GetComponent<Text>().text = container.Keys.ToArray()[0].Type;

        RefreshContainer(container);
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
            itemRenderer.GetComponentInParent<Button>().interactable = false;
            if (itemRenderer.GetComponentInParent<ItemScript>().assignedItem != null)
            {
                itemRenderer.GetComponentInParent<ItemScript>().assignedItem = null;
            }
        }
    }

    /// <summary>
    /// Une variation de DisplayDialogues, t'en occupe pas, t'en as pas besoin, mais tu peux me demander si jamais tu veux savoir commment je fais ça ;)
    /// </summary>
    /// <param name="dialogueType">Le type de dialogue souhaité</param>
    void DisplayDialogues(string dialogueType)
    {
        switch (dialogueType)
        {
        }
    }
    /// <summary>
    /// Une variation de DisplayDialogues, spécifique à l'affichage du loot
    /// </summary>
    /// <param name="loot">Le loot obtenue</param>
    public void DisplayDialogues(Item[] loot)
    {
        string textToDisplay = "You've found";
        foreach(Item item in loot)
        {
            textToDisplay += " 1 " + item.Name;
        }
        textToDisplay += ".";
        dialogues.text = textToDisplay;
    }
}
