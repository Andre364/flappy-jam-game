using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 goalPos; //The pos where the bird reaches the other side
    Rigidbody2D rb;
    public BirdManager bm;

    bool hasFinished = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(goalPos * moveSpeed * Time.deltaTime);
        rb.AddForce(goalPos * moveSpeed * 15f * Time.deltaTime, ForceMode2D.Force);
        if (rb.velocity.x > moveSpeed)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void Update()
    {
        if (transform.position.x >= goalPos.x && !hasFinished)
        {
            hasFinished = true;
            bm.RemoveHealth(gameObject);
        }
    }
}
