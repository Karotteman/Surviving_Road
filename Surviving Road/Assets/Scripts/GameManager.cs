using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame() 
    {
        LoadScene("HomeScene");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadTransition(sceneName));
    }

    IEnumerator LoadTransition(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    void GameOver() { }
}
