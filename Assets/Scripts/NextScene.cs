using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    MusicManager mm;

    public bool isInitializerScene;

    private void Start()
    {
        mm = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        if (isInitializerScene)
        {
            Next(1);
        }
    }

    public void Next(int index)
    {
        SceneManager.LoadScene(index);
        mm.SwitchMusic(index);
    }
}
