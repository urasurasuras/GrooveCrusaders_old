using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;

    private Rigidbody2D rb2d;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public bool charLosing;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enemy triggered?");
        Destroy(gameObject, 0f);

        if (other.gameObject.tag == "Proectile")
        {
            Debug.Log("projectiel hit enemy");
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
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            //rb2d.AddForce(target.position - transform.position);

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
