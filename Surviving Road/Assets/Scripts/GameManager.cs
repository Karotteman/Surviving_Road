using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator transitionAnim;

    public JsonManager jsonManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print("SICK : " + Player.Sickness);
    }

    public void NewGame()
    {
        Player.inGame = true;
        Inventory.foodStock = new Dictionary<Item, int>();
        Inventory.WaterStock = new Dictionary<Item, int>();
        Inventory.medpackStock = new Dictionary<Item, int>();
        Inventory.antibioticStock = new Dictionary<Item, int>();
        Inventory.weaponStock = new Dictionary<Item, int>();
        Inventory.protectionStock = new Dictionary<Item, int>();
        jsonManager.NewSave();
        LoadScene(1);
    }

    public void LoadScene(int sceneName)
    {
        StartCoroutine(LoadTransition(sceneName));
    }

    IEnumerator LoadTransition(int sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void GameOver() 
    {
        LoadScene(4);
        Player.inGame = false;
    }
}
