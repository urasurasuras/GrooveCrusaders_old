using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text sizeText;
    public Text winText;
    public Text loseText;

    public  AudioSource pickupSound, winSound, playSound, loseSound;

    private Rigidbody2D rb2d;
    private int count, size;

    private bool facingRight;

    public bool winCondition;


    
    public GameObject liteToRight, liteToLeft;
    Vector2 litePos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        facingRight = true;
        count = 0;
        size = 0;
        //winText.text = "";
        //loseText.text = "";
    }

    void FixedUpdate()
    {
        //float horMove = Input.GetAxis("Horizontal");
        //float verMove = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector2(horMove, verMove);
        //rb2d.AddForce(movement*speed);

        //7. Instead of adding force (like above) we use -/+ position to make more responsive movement while also implementing the "speed" variable
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
            facingRight = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
            facingRight = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if(Input.GetKey (KeyCode.Space)&& Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            fire();
        }
    }

    //public bool charPickingUp;
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("PickUp"))
    //    {
    //        other.gameObject.SetActive(false);
    //        count = count+1;
    //        pickupSound.Play();//11. Added an audio source component to the “Player” object that plays a sound each time an OnTriggerEnter2D method is called
    //        size = size -1;
    //        transform.localScale += new Vector3(-0.01F, -0.01F, 0);//9. Made the player’s scale slightly smaller on each pickup

    //    }
    //}

   
    // Update is called once per frame
    void Update()
    {
        //countText.text = "Count: " + count.ToString();
        //sizeText.text = "Size: " + size.ToString();
        //if (count >= 13)
        //{
        //    winText.text = "You Win !";
        //    winCondition = true;
        //}

        //if (winCondition==true)
        //{

        //    winSound.Play();
        //}
        
    }

    void fire()
    {
        transform.localScale += new Vector3(+0.01F, +0.01F, 0);
        size = size + 1;

        litePos = transform.position;
        if (facingRight)
        {
            litePos += new Vector2(+1.8f, 0f);
            Instantiate(liteToRight, litePos, Quaternion.identity);
        }
        else if (!facingRight)
        {
            litePos += new Vector2(-1.8f, 0f);
            Instantiate(liteToLeft, litePos, Quaternion.identity);
        }

    }
}
