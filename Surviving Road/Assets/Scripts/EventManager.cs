using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Sprite[] characterList;
    public GameObject characterSpace;

    private SpriteRenderer spriteR;

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

    //public void GetEvent()
}
