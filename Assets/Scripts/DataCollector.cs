using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public int timesSmashed;
    public int birdsKilled;
    public int heartsCollected;
    public int timeAlive;

    float seconds;
    int minutes;

    private void FixedUpdate()
    {
        seconds += 1f / 50f;
        if (seconds >= 60f)
        {
            seconds = 0f;
            minutes++;
        }
    }
}
