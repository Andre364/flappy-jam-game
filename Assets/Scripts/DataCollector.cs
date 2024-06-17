using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public int timesSmashed;
    public int birdsKilled;
    public int heartsCollected;
    public float killToSmashRatio;

    public float seconds;
    public int minutes;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        seconds += 1f / 50f;
        if (seconds >= 60f)
        {
            seconds = 0f;
            minutes++;
        }
        killToSmashRatio = (float)birdsKilled / (float)timesSmashed;
    }
}
