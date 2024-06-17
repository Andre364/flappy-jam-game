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

        Screen.fullScreen = true;

        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
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

    MusicManager mm;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isInSettings)
        {
            SceneManager.LoadScene(2);
            mm.SwitchMusic(2);
        }
    }
}
