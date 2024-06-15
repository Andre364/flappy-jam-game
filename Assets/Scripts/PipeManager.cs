using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] bool isUsingMouseAsInput;
    int moveDirection;
    public float moveSpeed;
    public LayerMask birdLayer;
    List<GameObject> birdsInRange = new List<GameObject>();
    Animator anim;

    bool hasPipeLoaded;

    private void Start()
    {
        hasPipeLoaded = false;
        anim = GetComponent<Animator>();
        Cursor.visible = false;
    }

    public void PipeLoaded()
    {
        anim.SetTrigger("idle");
        hasPipeLoaded = true;
    }

    private void Update()
    {
        if (isUsingMouseAsInput)
            MovePipeWithMouse();
        else
            MovePipeUsingDir();

        if (Input.GetKey(KeyCode.Space) && hasPipeLoaded)
            StartShootAnim();
    }

    void StartShootAnim()
    {
        hasPipeLoaded = false;
        anim.SetTrigger("shoot");
    }

    public void Shoot()
    {
        GameObject[] birdArray = birdsInRange.ToArray();
        foreach (GameObject bird in birdArray)
        {
            Destroy(bird);
        }
        birdsInRange.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bird>())
        {
            birdsInRange.Add(other.gameObject);
            //Debug.Log(birdsInRange.Count + " birds in range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (birdsInRange.Contains(other.gameObject))
        {
            birdsInRange.Remove(other.gameObject);
            //Debug.Log(birdsInRange.Count + " birds in range");
        }
    }

    public void MovePipeWithArrows(int dir)
    {
        if (!isUsingMouseAsInput)
        {
            moveDirection = dir;
        }
    }

    void MovePipeUsingDir()
    {
        float moveX = Mathf.Clamp(transform.position.x + moveDirection * moveSpeed * Time.deltaTime, -9, 9);
        Vector2 moveTo = new Vector2(moveX, 0);
        transform.position = moveTo;
    }
    


    void MovePipeWithMouse()
    {
        float newX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        newX = Mathf.Clamp(newX, -9f, 9f);

        //TODO: make Y pos smoother

        float newY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        newY *= Time.deltaTime * 5;

        newY = Mathf.Clamp(newY, -0.5f, 0.5f);

        Vector2 newPos = new Vector2(newX, newY);
        transform.position = newPos;
    }
}
