using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public Transform PlayerControl;
    public float velX;
    public Rigidbody2D rb;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * velX;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * velX * Time.deltaTime;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject, 0f);
    }
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    Destroy(gameObject, 0f);
    //}
}
