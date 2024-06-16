using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // IDEAS FOR DIFFICULTY OVER TIME:
    // Birds get faster
    // Birds accelerate faster?
    // Heart spawn chance decreases
    //

    public AnimationCurve curve;
    public float difficulty;
    float time;
    public int minutesToMaxDifficulty;

    float seconds;

    private void Start()
    {
        difficulty = 1f;
    }

    private void FixedUpdate()
    {
        seconds += 1f / 50f;
        if (seconds >= 1)
        {
            seconds = 0;
            CalculateDifficulty();
        }
    }

    void CalculateDifficulty()
    {
        float minute = 1f / 60f;
        time += minute / minutesToMaxDifficulty; // Time is in minutes.
        if (time <= 1)
        {
            difficulty = 1f + curve.Evaluate(time);
        }
    }
}
