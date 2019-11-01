using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class boss2 : MonoBehaviour
{
    public float speed=15;

    public Transform targetCharLocation;
    private Vector3 currentBossPos;
    private Vector3 lastPlayerPos;
    public Slider healtBar;
    public int bossHealth = 100;

    public bool moving;

    public GameObject boss2adObject;   //ad prefab
    public GameObject boss2adObjectX;   //ad prefab opposite direction
                                        //boss2add boss2addRef;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);

            if (bossHealth <= 0)
            {
                splitBoss2();
            }
            bossHealth -= 10;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        targetCharLocation = GameObject.Find("char0").GetComponent<Transform>();
        //boss2addRef = GetComponent<boss2add>();
        noteShooter.barEvent.AddListener(chargeToPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        noteShooter.barEvent.AddListener(setTarget);

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

    public void setTarget()
    {
        //Debug.Log("Set target: " + lastPlayerPos);
        lastPlayerPos = targetCharLocation.position;
        moving = true;
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
