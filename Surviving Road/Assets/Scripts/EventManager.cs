using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Sprite[] characterList;
    public GameObject characterSpace;
    public PlayerManager playerManager; // Utile pour utiliser un item, faire passer le temps ou simplement vérifier que le joueur est toujour vivant
    public InventoryManager inventoryManager; // Permet de manipuler facilement l'inventaire du joueur
    public UIManager uIManager; // La j'ai pas d'excuses, c'est juste que j'ai la flemme de faire autrement

    private SpriteRenderer spriteR;

    // Désolé, je m'incruste
    private int nbLootMin = 0;
    private int nbLootMax = 5;
    private float ennemiLife = 1; // IMMONDE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void Start()
    {
        //Add Random Character
        if (PlayerStats.actualEvent.Type != "None")
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
        playerManager.TimeSpent(PlayerStats.actualEvent.TimeCostInvest);
        int pokerFace = Random.Range(0, 100);

        if(pokerFace >= PlayerStats.actualEvent.ActionInvestigates[0])
        {
            string truth = PlayerStats.actualEvent.InvestigationDialogue[1];
            uIManager.DisplayInvestigationDialogues(truth);
        }
        else
        {
            string truth = PlayerStats.actualEvent.InvestigationDialogue[0];
            uIManager.DisplayInvestigationDialogues(truth);
        }
    }

    /// <summary>
    /// Resolve a Fight in EventScene
    /// </summary>
    public void Fight()
    {
        playerManager.TimeSpent(PlayerStats.actualEvent.TimeCostA);
        Item ennemiWeapon = PlayerStats.GetRandomItem("Weapon", 2);
        Item ennemiProtection = PlayerStats.GetRandomItem("Protection", 3);
        float damageGiven;
        float damageTaken = ennemiWeapon.Damage;
        if (PlayerStats.equippedProtection != null) damageTaken -= PlayerStats.equippedProtection.Protection;
        if (damageTaken < 0) damageTaken = 0;
        playerManager.SetHealth(-damageTaken);

        if (PlayerStats.equippedWeapon != null) damageGiven = PlayerStats.equippedWeapon.Damage;
        else damageGiven = 0.1f;
        ennemiLife -= damageGiven;

        playerManager.StillAlive();
        if (ennemiLife <= 0)
        {
            PlayerStats.actualEvent.Fight = false;
            spriteR.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().SwitchMusic("main");
        }
        else
        {
            string feedback;
            if (damageGiven > damageTaken)
            {
                feedback = PlayerStats.results[4].Text[Random.Range(0, PlayerStats.results[4].Text.Length)];
            }
            else
            {
                feedback = PlayerStats.results[3].Text[Random.Range(0, PlayerStats.results[3].Text.Length)];
            }
            uIManager.DisplayInvestigationDialogues(feedback);
        }

    }

    /// <summary>
    /// Reaction to pnj, generate another reaction
    /// </summary>
    public void Reaction()
    {

    }

    /// <summary>
    /// Add a list of Item into the inventory of the player
    /// </summary>
    public void Loot()
    {
        Item[] loot = GetLoot();
        foreach(Item item in loot)
        {
            inventoryManager.Pickup(item);
        }
        uIManager.DisplayDialogues(loot);
        PlayerStats.actualEvent.ActionSearch = false;
        playerManager.TimeSpent(PlayerStats.actualEvent.TimeCostA);
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
        for (int i =0; i < PlayerStats.actualLocation.LootRate.Length; i++)
        {
            Item[] tempItemArray = PlayerStats.GetItemList(PlayerStats.actualLocation.LootRate[i].Loot);
            for(int k = 0; k < PlayerStats.actualLocation.LootRate[i].DropRate; k++)
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
