using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class enemyFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance=.01f;
    public static UnityEvent enemyExplodingOnPlayer;

    public bool isAlive = true;

    private Rigidbody2D rb2d;

    private Transform targetPosition;
    public GameObject targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        GameObject targetPlayer = GameObject.Find("char0");
    }

    public bool charLosing;
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("enemy triggered?");
        //Destroy(gameObject, 0f);

        if(other.gameObject.tag == "Enemy") { }     //do nothing if triggered by enemy
        if (other.gameObject.tag == "Projectile" || other.gameObject.tag == "Player")
        {
            isAlive = false;
            Debug.Log("enemy is alive: " + isAlive);
            Destroy(gameObject, 0f);
        }
        /*
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("char0") != null)
        {
            targetPlayer = GameObject.Find("char0");
            targetPosition = targetPlayer.GetComponent<Transform>();
        }else if(GameObject.Find("char1") != null)
        {
            targetPlayer = GameObject.Find("char1");
            targetPosition = targetPlayer.GetComponent<Transform>();
        }else if(GameObject.Find("char2") != null)
        {
            targetPlayer = GameObject.Find("char2");
            targetPosition = targetPlayer.GetComponent<Transform>();
        }

        if (targetPlayer != null){
            if (Vector2.Distance(transform.position, targetPosition.position) > 0)
            {
                //Debug.Log(target);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
            }
        }
        
    }
}
