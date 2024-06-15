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

            //Generate new bird spawn time
            float nextTimer = 2 + Random.Range(-1f, 0.25f);

            yield return new WaitForSeconds(nextTimer);
        }
    }

    void SpawnBird()
    {
        Vector2 spawnP;
        Vector2 endP;
        
        GameObject newBird = Instantiate(bird);
        Bird nb = newBird.GetComponent<Bird>();
        
        float r = Random.value;
        if (r >= 0.5f)
        {
            nb.isFacingRight = true;
            spawnP = birdSpawnPoint.position;
            endP = birdGoalPos.position;
        }
        else
        {
            nb.isFacingRight = false;
            spawnP = birdGoalPos.position;
            endP = birdSpawnPoint.position;
        }

        newBird.transform.position = spawnP;
        nb.goalPos = endP;
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
