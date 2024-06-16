using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleSuicide : MonoBehaviour
{
    public int timeToDeath;

    void Start()
    {
        Invoke("Die", timeToDeath);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
