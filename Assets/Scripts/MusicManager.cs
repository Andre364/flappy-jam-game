using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    void Start()
    {
        game.Stop();
        game.volume = 0f;

        DontDestroyOnLoad(gameObject);
    }

    public AudioSource menu;
    public AudioSource game;
    public float vol;

    AudioSource auFrom;
    AudioSource auTo;

    public void SwitchMusic(int index)
    {
        StartCoroutine("CrossFade", index);
    }

    public float crossFadeDuration;
    public int crossFadeLoops;
    public float timeBeforeTo;

    IEnumerator CrossFade(int index)
    {
        auFrom = menu;
        auTo = game;

        switch (index)
        {
            case 1: //To menu
                auFrom = menu;
                auTo = menu;
                break;
            case 2: //To game
                auFrom = menu;
                auTo = game;
                break;
            case 3: //To game over screen
                auFrom = game;
                auTo = menu;
                break;
        }

        if (auTo != auFrom)
        {
            StartCoroutine("CrossFadeFrom");
            yield return new WaitForSeconds(timeBeforeTo);
            StartCoroutine("CrossFadeTo");

        }
        yield return null;
    }
    IEnumerator CrossFadeFrom()
    {
        AudioSource au = auFrom;

        float time = 0f;
        float add = vol * (crossFadeDuration / crossFadeLoops);

        for (int i = 0; i < crossFadeLoops; i++)
        {
            au.volume -= add;

            time += crossFadeDuration / crossFadeLoops;
            yield return new WaitForSeconds(crossFadeDuration / crossFadeLoops);
        }
        au.Stop();
    }
    IEnumerator CrossFadeTo()
    {
        AudioSource au = auTo;
        au.Play();

        float time = 0f;
        float add = vol * (crossFadeDuration / crossFadeLoops);

        for (int i = 0; i < crossFadeLoops; i++)
        {
            au.volume += add;

            time += crossFadeDuration / crossFadeLoops;
            yield return new WaitForSeconds(crossFadeDuration / crossFadeLoops);
        }
    }
}
