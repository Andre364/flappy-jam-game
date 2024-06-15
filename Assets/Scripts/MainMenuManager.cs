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
    }

    public void OpenSettings(bool open)
    {
        isInSettings = open;

        if (open)
        {
            //open menu
        }
        else
        {
            //close menu
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
