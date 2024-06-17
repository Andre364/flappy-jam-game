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
        isCrossfadeNow = false;
    }

    public AudioSource menu;
    public AudioSource game;
    public AudioSource gameOver;
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

    bool isCrossfadeNow;

    IEnumerator CrossFade(int index)
    {
        auFrom = menu;
        auTo = game;

        switch (index)
        {
            case 1: //To menu from game over
                auFrom = gameOver;
                auTo = menu;
                break;
            case 2: //To game
                auFrom = menu;
                auTo = game;
                break;
            case 3: //To game over screen
                auFrom = game;
                auTo = gameOver;
                break;
        }

        if (auTo != auFrom)
        {
            while (isCrossfadeNow)
            {
                yield return new WaitForSeconds(0.1f) ;
            }

            isCrossfadeNow = true;

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
        isCrossfadeNow = false;
    }
}
