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
    public float bossHealth = 100;

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
            bossHealth -= (float)other.gameObject.GetComponent<projectile>().base_value;
        }
        if (other.gameObject.tag == "f_aeo")
        {
            moving = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        moving = false;

        targetPlayer = GameObject.Find("char0");
        targetCharLocation = targetPlayer.GetComponent<Transform>();
        //boss2addRef = GetComponent<boss2add>();
        noteShooter.barEvent.AddListener(chargeToPlayer);
    }

    // Update is called once per frame
    void Update()
    {
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
