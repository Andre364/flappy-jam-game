using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public GameObject bird;

    public Transform birdSpawnPoint;
    public Transform birdGoalPos;

    public bool spawnBirds = true;

    public int health;

    [Header("Bird config")]
    public float birdDefaultSpeed;

    private void Start()
    {
        StartCoroutine("BirdSpawner");
    }

    IEnumerator BirdSpawner()
    {
        while (spawnBirds)
        {
            SpawnBird();
            yield return new WaitForSeconds(2f);
        }
    }

    void SpawnBird()
    {
        GameObject newBird = Instantiate(bird);
        newBird.transform.position = birdSpawnPoint.position;
        Bird nb = newBird.GetComponent<Bird>();
        nb.goalPos = birdGoalPos.position;
        nb.moveSpeed = birdDefaultSpeed;
        nb.bm = this;
    }

    public void RemoveHealth(GameObject attackerBird)
    {
        health--;
        Debug.Log("Lost 1hp.. lousy ass");
        Destroy(attackerBird);
    }
}
