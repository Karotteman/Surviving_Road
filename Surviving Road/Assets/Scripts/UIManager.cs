using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject containerPanel;
    public GameObject sleepWarningPanel;

    //public bool SetContainerDisplay { set { containerPanel.active; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayContainer()
    {
        if (sleepWarningPanel.activeSelf)
        {
            sleepWarningPanel.SetActive(false);
        }
        containerPanel.SetActive(!containerPanel.activeSelf);
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
}
