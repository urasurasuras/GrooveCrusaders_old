using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class boss2 : MonoBehaviour
{
    public float speed=15;

    public GameObject targetPlayer;

    public Transform targetCharLocation;

    private Vector3 currentBossPos;
    private Vector3 lastPlayerPos;
    public Slider healtBar;
    public float maxHealth = 100;
    public float bossHealth;


    //EQ slider values
    float frequency=93/8, magnitude=.5f;
    public Slider s1, s2, s3, s4, s5, s6;
    public Color start_color, mid_color,end_color;
    Color color_cache;
    int divident = 32;
    static float time_cache;


    public bool moving;

    public GameObject boss2adObject;   //ad prefab
    public GameObject boss2adObjectX;   //ad prefab opposite direction
                                        //boss2add boss2addRef;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "f_damage")
        {
            Destroy(other.gameObject);

            if (bossHealth <= 0)
            {
                splitBoss2();
            }
            bossHealth -= (float)other.gameObject.GetComponent<projectile>().value_final;
        }
        if (other.gameObject.tag == "f_aoe")
        {
            moving = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        bossHealth = maxHealth;
        moving = false;

        targetPlayer = GameObject.Find("char0");
        targetCharLocation = targetPlayer.GetComponent<Transform>();
        //boss2addRef = GetComponent<boss2add>();
        noteShooter.barEvent.AddListener(chargeToPlayer);
        time_cache = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        //print(noteShooter.marker);
        magnitude = (bossHealth / maxHealth);
        //print(Mathf.Sin(Time.time/60*noteShooter.bpm)*magnitude);
        //DEBUG
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            bossHealth -= 5;
        }
        //print(magnitude);
        moving = false;
        if (magnitude > .5)
        {
            color_cache = Color.Lerp(mid_color, start_color, magnitude);
        }
        else if (magnitude <= .5)
        {
            color_cache = Color.Lerp(end_color, color_cache, magnitude);
        }
        if (noteShooter.marker == "Beat 1" || noteShooter.marker == "Beat 2" || noteShooter.marker == "Beat 3" || noteShooter.marker == "Beat 4" || noteShooter.marker == "Beat 5" || noteShooter.marker == "Beat 6")
        {
            s1.value = Mathf.Sin(Time.time*noteShooter.bpm *Mathf.PI/60) * magnitude ;
            s1.gameObject.transform.Find("Handle Slide Area").Find("Handle").GetComponent<Image>().color = color_cache;
        }
        if (noteShooter.marker == "Beat 2" || noteShooter.marker == "Beat 3" || noteShooter.marker == "Beat 4" || noteShooter.marker == "Beat 5" || noteShooter.marker == "Beat 6")
        {
            s2.value = Mathf.Sin((Time.time * noteShooter.bpm * Mathf.PI / 60) + Mathf.PI) * magnitude;
        }
        if (noteShooter.marker == "Beat 3" || noteShooter.marker == "Beat 4" || noteShooter.marker == "Beat 5" || noteShooter.marker == "Beat 6")
            s3.value = Mathf.Sin((Time.time * noteShooter.bpm * Mathf.PI / 60) + Mathf.PI*2) * magnitude;
        if (noteShooter.marker == "Beat 4" || noteShooter.marker == "Beat 5" || noteShooter.marker == "Beat 6")
            s4.value = Mathf.Sin((Time.time * noteShooter.bpm * Mathf.PI / 60) + Mathf.PI*3) * magnitude;
        if (noteShooter.marker == "Beat 5" || noteShooter.marker == "Beat 6")
            s5.value = Mathf.Sin((Time.time * noteShooter.bpm * Mathf.PI / 60) + Mathf.PI*4) * magnitude;
        if (noteShooter.marker == "Beat 6")
            s6.value = Mathf.Sin((Time.time * noteShooter.bpm * Mathf.PI / 60) + Mathf.PI*5) * magnitude;
        //print(temp);
        if (!GameManager.Instance.loss)
        {
            noteShooter.barEvent.AddListener(setTarget);

            if (GameObject.Find("char0") != null)
            {
                targetPlayer = GameObject.Find("char0");
                targetCharLocation = targetPlayer.GetComponent<Transform>();
            }
            else if (GameObject.Find("char1") != null)
            {
                targetPlayer = GameObject.Find("char1");
                targetCharLocation = targetPlayer.GetComponent<Transform>();
            }
            else if (GameObject.Find("char2") != null)
            {
                targetPlayer = GameObject.Find("char2");
                targetCharLocation = targetPlayer.GetComponent<Transform>();
            }
            else
            {
                GameManager.Instance.loss = true;
            }
            healtBar.value = bossHealth;
            if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("pressed F");
                splitBoss2();
            }


            if (moving)
            {
                chargeToPlayer();
            }
        }
    }

    public void setTarget()
    {
        if (!GameManager.Instance.loss)
        {
            //Debug.Log("Set target: " + lastPlayerPos);
            lastPlayerPos = targetCharLocation.position;
            moving = true;
        }
    }
    public void chargeToPlayer()
    {
        //Debug.Log("in charge func");
        //Debug.Log(lastPlayerPos);
        //Debug.Log(transform.position);

        if (transform.position != lastPlayerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPlayerPos, speed * Time.deltaTime);
        }
    }

    void splitBoss2()
    {
        Instantiate(boss2adObject, transform.position, transform.rotation);
        //boss2addRef.diagonalDirection = 30;
        Instantiate(boss2adObjectX, transform.position, transform.rotation);
        //boss2addRef.diagonalDirection = -30;

        Destroy(this, 0f);
        gameObject.SetActive(false);
    }
}
