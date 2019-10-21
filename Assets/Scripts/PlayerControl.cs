using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    
    public static UnityEvent playerTouchingEnemy;
    public double playerHealth =100;
    public bool gameStarted;
    Animator drummerAnimationController;

    private bool isDrummerStationary;


    //CONTROLS
    [SerializeField] private Player1controls _controls;


    private void OnEnable()
    {
        _controls.player1.South.performed += HandleSouth;
        _controls.player1.South.Enable();

        _controls.player1.East.performed += HandleEast;
        _controls.player1.East.Enable();

        _controls.player1.North.performed += HandleNorth;
        _controls.player1.North.Enable();

        _controls.player1.West.performed += HandleWest;
        _controls.player1.West.Enable();
    }


    private void HandleSouth(InputAction.CallbackContext context)
    {
        Debug.Log("South");
    }
    private void HandleWest(InputAction.CallbackContext obj)
    {
        Debug.Log("West");
    }
    private void HandleNorth(InputAction.CallbackContext obj)
    {
        Debug.Log("North");
    }
    private void HandleEast(InputAction.CallbackContext obj)
    {
        Debug.Log("East");
    }

    private void OnDisable()
    {
        _controls.player1.South.performed -= HandleSouth;
        _controls.player1.South.Disable();

        _controls.player1.East.performed -= HandleEast;
        _controls.player1.East.Disable();

        _controls.player1.North.performed -= HandleNorth;
        _controls.player1.North.Disable();

        _controls.player1.West.performed -= HandleWest;
        _controls.player1.West.Disable();
    }





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
    public bool playerCanFire;


    // Start is called before the first frame update
    void Start()
    {
        weaponGuitar weapon_guitar = GetComponent<weaponGuitar>();
        weaponDrumset weapon_drumset = GetComponent<weaponDrumset>();

        playerTouchingEnemy = new UnityEvent();
        GameManager.Instance.RegisterPlayerControl(this);
        gameStarted = false;
        rb2d = GetComponent<Rigidbody2D>();
        facingRight = true;
        count = 0;
        size = 0;
        //winText.text = "";
        //loseText.text = "";
        isDrummerStationary = false;
        drummerAnimationController = GetComponent<Animator>();

    }

    void FixedUpdate()
    {
        //float horMove = Input.GetAxis("Horizontal");
        //float verMove = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector2(horMove, verMove);
        //rb2d.AddForce(movement*speed);

        //7. Instead of adding force (like above) we use -/+ position to make more responsive movement while also implementing the "speed" variable

        playerTouchingEnemy.Invoke();

        if (Input.GetKey(KeyCode.A) && !isDrummerStationary)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
            facingRight = false;
        }
        if (Input.GetKey(KeyCode.D) && !isDrummerStationary)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
            facingRight = true;
        }
        if (Input.GetKey(KeyCode.W) && !isDrummerStationary)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && !isDrummerStationary)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            //Debug.Log("drummer toggle key pressed");
            if (!isDrummerStationary)
            {
                //Debug.Log("inside if stationary");

                setDrummerStationary();
            }
            else if (isDrummerStationary)
            {
                //Debug.Log("inside if walking");

                setDrummerWalking();
            }
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
        //Debug.Log(playerCanFire);
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
            }
        }
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }


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
    public void setDrummerStationary()
    {
        //Debug.Log("Stationary: " + drummerAnimationController.GetBool("isStationary"));

        drummerAnimationController.SetBool("isStationary", true);
        isDrummerStationary = true;
        Debug.Log(isDrummerStationary);
    }
    public void setDrummerWalking()
    {
        //Debug.Log("Stationary: " + drummerAnimationController.GetBool("isStationary"));

        drummerAnimationController.SetBool("isStationary", false);
        isDrummerStationary = false;
        Debug.Log(isDrummerStationary);

    }
    private void dmgOverTime()
    {
        Debug.Log(playerHealth);
        playerHealth -= 0.02;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("touching boss");
            playerTouchingEnemy.AddListener(dmgOverTime);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("not touching enemy anymore");
            playerTouchingEnemy.RemoveListener(dmgOverTime);
        }
    }

}
