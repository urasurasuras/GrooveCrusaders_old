using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class enemyFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public static UnityEvent enemyExplodingOnPlayer;

    public bool isAlive = true;

    private Rigidbody2D rb2d;

    private Transform target;
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
        //Debug.Log(target);
        //Debug.Log(targetPlayer);
        
        target = GameObject.Find("char0").GetComponent<Transform>();
        
        if (target == null)
        {
            target = GameObject.Find("char1").GetComponent<Transform>();
        }else if (target == null)
        {
            target = GameObject.Find("char2").GetComponent<Transform>();
        }
        else { }
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            //Debug.Log(target);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
