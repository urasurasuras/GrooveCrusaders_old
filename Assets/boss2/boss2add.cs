using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2add : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(30 * speed, 30 * speed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
