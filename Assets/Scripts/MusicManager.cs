using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        menu.volume = vol;
        game.volume = 0;
    }

    public AudioSource menu;
    public AudioSource game;
    public float vol;

    public void SwitchMusic(int index)
    {
        if (index == 0 || index == 2)
        {
            StartCoroutine("CrossFade", menu);
        }
        else if (index == 1)
        {
            StartCoroutine("CrossFade", game);
            //menu.volume = 0;
            //game.volume = vol;
        }
    }

    public float crossFadeDuration;
    public int crossFadeLoops;

    IEnumerator CrossFade(AudioSource auTo)
    {
        AudioSource auFrom;

        if (auTo == game)
        {
            auFrom = menu;
        }
        else
        {
            auFrom = game;
        }

        float time = 0f;
        float add = vol * (crossFadeDuration / crossFadeLoops);

        while (time < crossFadeDuration)
        {
            for (int i = 0; i < crossFadeLoops; i++)
            {

                auTo.volume += add;
                auFrom.volume -= add;

                time += crossFadeDuration / crossFadeLoops;
                Debug.Log(time);
                yield return new WaitForSeconds(crossFadeDuration / crossFadeLoops);
            }
            yield return null;
        }
    }
}
