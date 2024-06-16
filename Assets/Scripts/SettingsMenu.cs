using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public List<Slider> volumeSliders;
    public List<AudioMixerGroup> volumeGroups;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < volumeSliders.Count; i++)
        {
            volumeSliders[i].value = PlayerPrefs.GetFloat("Slider" + i.ToString(), 0.8f);
            ChangeVolume(i);
        }
    }

    public void ChangeVolume(int index) //0 is master, 1 is music, 2 is sfx
    {
        Slider s = volumeSliders[index];
        AudioMixerGroup g = volumeGroups[index];

        PlayerPrefs.SetFloat("Slider" + index.ToString(), s.value);
        mixer.SetFloat(g.name, Mathf.Log10(s.value) * 20);
    }
}
