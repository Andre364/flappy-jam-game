using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    bool isInSettings;

    private void Start()
    {
        isInSettings = false;
        settingsMenu.SetActive(false);
    }

    public GameObject settingsMenu;

    public void OpenSettings(bool open)
    {
        isInSettings = open;

        if (open)
        {
            //open menu
            settingsMenu.SetActive(true);
        }
        else
        {
            //close menu
            settingsMenu.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isInSettings)
        {
            SceneManager.LoadScene(0);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }
    }
}
