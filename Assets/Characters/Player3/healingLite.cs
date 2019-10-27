using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingLite : MonoBehaviour
{
    public float velX;
    public Rigidbody2D rb;

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
}
