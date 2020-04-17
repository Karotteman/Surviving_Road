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
        print("SICK : " + PlayerStats.sickness);
        print("Fuel : " + PlayerStats.fuelStock);
    }

    public void NewGame()
    {
        PlayerStats.inGame = true;
        PlayerStats.foodStock = new Dictionary<Item, int>();
        PlayerStats.WaterStock = new Dictionary<Item, int>();
        PlayerStats.medpackStock = new Dictionary<Item, int>();
        PlayerStats.antibioticStock = new Dictionary<Item, int>();
        PlayerStats.weaponStock = new Dictionary<Item, int>();
        PlayerStats.protectionStock = new Dictionary<Item, int>();
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
        PlayerStats.inGame = false;
    }
}
