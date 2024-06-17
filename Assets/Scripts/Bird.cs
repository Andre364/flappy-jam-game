using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 goalPos; //The pos where the bird reaches the other side
    Rigidbody2D rb;
    public BirdManager bm;

    public bool isFacingRight;
    bool hasFinished = false;

    public SpriteRenderer sprite;
    public float acceleration;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!isFacingRight)
        {
            sprite.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(goalPos * moveSpeed * acceleration * Time.deltaTime, ForceMode2D.Force);
        if (rb.velocity.x > moveSpeed)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else if (rb.velocity.x < -moveSpeed)
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    private void Update()
    {
        if (!hasFinished)
        {
            if (isFacingRight && transform.position.x >= goalPos.x)
            {
                hasFinished = true;
                bm.RemoveHealth(gameObject);
            }
            else if (!isFacingRight && transform.position.x <= goalPos.x)
            {
                hasFinished = true;
                bm.RemoveHealth(gameObject);
            }
        }
    }
}
