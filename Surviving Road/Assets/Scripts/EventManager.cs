using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Sprite[] characterList;
    public GameObject characterSpace;
    public PlayerManager playerManager; // Utile pour utiliser un item, faire passer le temps ou simplement vérifier que le joueur est toujour vivant
    public UIManager uIManager; // La j'ai pas d'excuses, c'est juste que j'ai la flemme de faire autrement

    private SpriteRenderer spriteR;

    // Désolé, je m'incruste
    private int nbLootMin = 0;
    private int nbLootMax = 5;
    private int nbLostItemMax = 3;
    private int nbInvesgation = 0;
    private float ennemiLife = 1; // IMMONDE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void Start()
    {
        //Add Random Character
        if (Player.actualEvent.Type != "None")
        {
            Sprite Encounter = RandoCharacter();
            spriteR = characterSpace.GetComponent<SpriteRenderer>();
            spriteR.sprite = Encounter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Sprite RandoCharacter()
    {
        int randomCharacter = Random.Range(0, characterList.Length);
        return characterList[randomCharacter];
    }

    //HERE --- It works but I can't have it display the text
    public void Investigate()
    {
        Player.TimeSpent(Player.actualEvent.TimeCostInvest);
        int pokerFace = Random.Range(0, 100);
        string truth;

        if (pokerFace <= Player.actualEvent.ActionInvestigates[nbInvesgation])
        {
            truth = Player.actualEvent.InvestigationDialogue[1];
            Player.actualEvent.ActionInvestigates = null;
        }
        else
        {
            truth = Player.actualEvent.InvestigationDialogue[0];
            nbInvesgation++;
        }
        uIManager.DisplayDialogues(truth);
        uIManager.DisplayButtons();
        playerManager.StillAlive();
    }

    /// <summary>
    /// Resolve a Fight in EventScene
    /// </summary>
    public void Fight()
    {
        Player.TimeSpent(Player.actualEvent.TimeCostA);
        Item ennemiWeapon = Player.GetRandomItem("Weapon", 2);
        Item ennemiProtection = Player.GetRandomItem("Protection", 3);
        float damageGiven;
        float damageTaken = ennemiWeapon.Damage;
        if (Player.equippedProtection != null) damageTaken -= Player.equippedProtection.Protection;
        if (damageTaken < 0) damageTaken = 0;
        Player.Health -= damageTaken;

        if (Player.equippedWeapon != null) damageGiven = Player.equippedWeapon.Damage;
        else damageGiven = 0.1f;
        ennemiLife -= damageGiven;

        print("TAKEN : " + damageTaken + "  ||  GIVEN : " + damageGiven);

        string feedback;
        playerManager.StillAlive();
        if (ennemiLife <= 0)
        {
            Player.actualEvent.Fight = false;
            spriteR.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().SwitchMusic("main");
            feedback = Player.results[4].Text[Random.Range(0, Player.results[4].Text.Length)];
        }
        else
        {
            if (damageGiven > damageTaken)
            {
                feedback = Player.results[4].Text[Random.Range(0, Player.results[4].Text.Length)];
            }
            else
            {
                feedback = Player.results[3].Text[Random.Range(0, Player.results[3].Text.Length)];
            }
            uIManager.DisplayDialogues(feedback);
        }
        uIManager.DisplayButtons();
    }

    /// <summary>
    /// Reaction to pnj, generate another reaction
    /// </summary>
    public void Reaction(string reaction)
    {
        string feedback;
        feedback = Player.results[8].Text[Random.Range(0, Player.results[8].Text.Length)];
        switch (reaction)
        {
            case "accept":
                switch (Player.actualEvent.actionAccept)
                {
                    case "Good":
                        feedback = Player.results[1].Text[Random.Range(0, Player.results[4].Text.Length)];
                        Loot();
                        break;
                    case "Neutral":
                        feedback = Player.results[8].Text[Random.Range(0, Player.results[8].Text.Length)];
                        break;
                    case "Bad":
                        feedback = Player.results[5].Text[Random.Range(0, Player.results[5].Text.Length)];
                        LooseItem();
                        break;
                }
                break;
            case "refuse":
                switch (Player.actualEvent.actionRefused)
                {
                    case "GoodBluff":
                        feedback = Player.results[2].Text[Random.Range(0, Player.results[2].Text.Length)];
                        Loot();
                        break;
                    case "Neutral":
                        feedback = Player.results[8].Text[Random.Range(0, Player.results[8].Text.Length)];
                        break;
                    case "BadBluff":
                        feedback = Player.results[7].Text[Random.Range(0, Player.results[7].Text.Length)];
                        LooseItem();
                        Player.actualEvent.Fight = true;
                        break;
                }
                break;
            case "leave":
                switch (Player.actualEvent.actionRefused)
                {
                    case "Neutral":
                        feedback = Player.results[8].Text[Random.Range(0, Player.results[8].Text.Length)];
                        break;
                }
                break;
            default :
                feedback = Player.results[8].Text[Random.Range(0, Player.results[8].Text.Length)];
                break;
        }
        Player.TimeSpent(Player.actualEvent.TimeCostA);
        playerManager.StillAlive();
        uIManager.DisplayDialogues(feedback);
        Player.actualEvent.Resolved = true;
        uIManager.DisplayButtons();
    }

    /// <summary>
    /// Remove a list of Item into the inventory of the player
    /// </summary>
    public void LooseItem()
    {
        Item[] lostItem = new Item[Random.Range(1, nbLostItemMax)];
        List<Item> lostableItem = new List<Item>();
        foreach(KeyValuePair<Item, int> entry in Inventory.GetContainer("All"))
        {
            for (int j = 0; j < entry.Value; j++)
            {
                if(Player.equippedWeapon != entry.Key && Player.equippedProtection != entry.Key)
                {
                    lostableItem.Add(entry.Key);
                }
            }
        }

        for (int i = 0; i < lostItem.Length; i++)
        {
            int y = Random.Range(0, lostableItem.Count);
            lostItem[i] = lostableItem[y];
            Inventory.Remove(lostableItem[y]);
        }
        uIManager.DisplayDialogues(lostItem);
    }

    /// <summary>
    /// Add a list of Item into the inventory of the player
    /// </summary>
    public void Loot()
    {
        Item[] loot = GetLoot();
        foreach (Item item in loot)
        {
            Inventory.Pickup(item);
        }
        uIManager.DisplayDialogues(loot);
        Player.actualEvent.ActionSearch = false;
        Player.TimeSpent(Player.actualEvent.TimeCostA);
        playerManager.StillAlive();
        uIManager.DisplayButtons();
    }

    /// <summary>
    /// Generate list of Item Looted
    /// </summary>
    /// <returns></returns>
    Item[] GetLoot()
    {
        Item[] loot = new Item[Random.Range(nbLootMin, nbLootMax)];
        List<List<Item>> lootableItem = new List<List<Item>>();
        for (int i =0; i < Player.actualLocation.LootRate.Length; i++)
        {
            Item[] tempItemArray = Player.GetItemList(Player.actualLocation.LootRate[i].Loot);
            for(int k = 0; k < Player.actualLocation.LootRate[i].DropRate; k++)
            {
                lootableItem.Add(new List<Item>());
                for (int j = 0; j < tempItemArray.Length - 1; j++)
                {
                    for (int g = 0; g < tempItemArray[j].DropRate; g++)
                    {
                        lootableItem[k].Add(tempItemArray[j]);
                    }
                }
            }
        }

        for(int i = 0; i < loot.Length; i++)
        {
            int x;
            do
            {
                x = Random.Range(0, lootableItem.Count);
            }
            while (lootableItem[x].Count == 0);
            int y = Random.Range(0, lootableItem[x].Count);
            loot[i] = lootableItem[x][y];
        }

        return loot;
    }
}
