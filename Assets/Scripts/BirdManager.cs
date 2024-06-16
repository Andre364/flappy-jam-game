using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdManager : MonoBehaviour
{
    public GameObject bird;

    public Transform birdSpawnPoint;
    public Transform birdGoalPos;

    public bool spawnBirds = true;

    public float birdDefaultSpeed;

    private void Start()
    {
        StartCoroutine("BirdSpawner");
        health = maxHealth;
        ChangeHealthSprite();
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

    [Header("Health")]
    int health;
    public int maxHealth;
    public List<GameObject> hearts;

    public void RemoveHealth(GameObject attackerBird)
    {
        health--;
        Destroy(attackerBird);
        ChangeHealthSprite();
    }

    public void AddHealth()
    {
        if (health < maxHealth)
        {
            health++;
            ChangeHealthSprite();
        }
    }

    void ChangeHealthSprite()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i + 1 <= health)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
