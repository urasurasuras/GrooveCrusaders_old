using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drummerPulse : MonoBehaviour
{
    public float growSpeed;
    public Rigidbody2D rb;
    private CircleCollider2D drummerPulseCollider;
    private float circleScale;

    /*
public float x = 0.1f;
public float y = 0.1f;
public float z = 0.1f;
*/
    void Start()
    {
        this.drummerPulseCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        // Widen the object by x, y, and z values
        transform.localScale += new Vector3(growSpeed, growSpeed, growSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
