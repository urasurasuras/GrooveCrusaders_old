using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2add : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int diagonalDirection;
    public GameObject boss2ad2Object;   //ad prefab
    public GameObject boss2ad2ObjectX;   //ad prefab

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(diagonalDirection * speed, diagonalDirection * speed, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) )
        {
            splitBoss2();
        }
    }
    void splitBoss2()
    {
        Instantiate(boss2ad2Object, transform.position, transform.rotation);
        Instantiate(boss2ad2ObjectX, transform.position, transform.rotation);
        Destroy(this, 0f);
        gameObject.SetActive(false);
    }
}
