using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject blood;

    public AudioSource smashSound;
    public AudioSource smashHitSound;
    public AudioSource reloadSound;
    public AudioSource breath;

    public BirdManager bm;
    public GameObject heart;
    List<GameObject> heartsInRange = new List<GameObject>();

    public DataCollector dataCollector;

    [Header("Pipe Ammo")]
    public int startingPipes;
    int currentPipes;
    public TextMeshProUGUI pipeDisplay;
    public int maxPipesStored; //Including the pipe currently in use
    public float pipeGainInterval;
    bool isReloadTime;

    private void Start()
    {
        hasPipeLoaded = false;
        anim = GetComponent<Animator>();
        Cursor.visible = false;
        isReloadTime = true;

        currentPipes = startingPipes;
        StartCoroutine("RefillPipes");
    }

    IEnumerator RefillPipes()
    {
        while (isReloadTime)
        {
            if (currentPipes < maxPipesStored)
            {
                yield return new WaitForSeconds(pipeGainInterval);
                currentPipes++;
                if (currentPipes == 1)
                {
                    reloadSound.pitch = Random.Range(0.95f, 1.05f);
                    reloadSound.Play();
                    LoadNewPipe();
                    yield return null;
                }
            }
            yield return null;
        }
        yield return null;
    }

    public void PipeLoaded()
    {
        anim.SetTrigger("idle");
        hasPipeLoaded = true;
    }

    public void LoadNewPipe()
    {
        anim.SetTrigger("load");
    }

    private void Update()
    {
        if (isUsingMouseAsInput)
            MovePipeWithMouse();
        else
            MovePipeUsingDir();

        if (Input.GetKey(KeyCode.Mouse0) && hasPipeLoaded && currentPipes > 0 && bm.health > 0)
            StartShootAnim();
        pipeDisplay.SetText(currentPipes.ToString());
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
            GameObject b = Instantiate(blood);
            b.transform.position = transform.position;

            float f = Random.value;
            if (f <= 0.07f)
            {
                GameObject h = Instantiate(heart);
                h.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-10f, 10f));
                h.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(-0.25f, 0.25f));
            }
            dataCollector.birdsKilled++;
        }

        GameObject[] heartArray = heartsInRange.ToArray();

        if (heartArray.Length > 0)
            breath.Play();

        foreach (GameObject h in heartArray)
        {
            Destroy(h);
            bm.AddHealth();
            dataCollector.heartsCollected++;
        }
        heartsInRange.Clear();

        AudioSource au;

        if (birdArray.Length > 0)
            au = smashHitSound;
        else
            au = smashSound;
        au.pitch = Random.Range(0.95f, 1.05f);
        au.Play();
        
        birdsInRange.Clear();

        currentPipes--;

        dataCollector.timesSmashed++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bird>())
        {
            birdsInRange.Add(other.gameObject);
            //Debug.Log(birdsInRange.Count + " birds in range");
        }
        else if (other.gameObject.name.Contains("Heart"))
        {
            heartsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (birdsInRange.Contains(other.gameObject))
        {
            birdsInRange.Remove(other.gameObject);
            //Debug.Log(birdsInRange.Count + " birds in range");
        }
        else if (other.gameObject.name.Contains("Heart"))
        {
            heartsInRange.Remove(other.gameObject);
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

        //float newY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        //newY *= Time.deltaTime * 15;

        //newY = Mathf.Clamp(newY, -0.5f, 0.5f);

        Vector2 newPos = new Vector2(newX, 0);
        transform.position = newPos;
    }
}
