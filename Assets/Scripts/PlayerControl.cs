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
    public double playerHealth = 100;
    public bool gameStarted;
    Animator drummerAnimationController;

    healingLite healingProj;

    private bool isDrummerStationary;

    public GameObject char0;
    public GameObject char1;
    public GameObject char2;

    string charID;

    tutorialTexts tutorialText;
    public bool playerFiredTut;
    public bool playerHitTut;

    //CONTROLS
    //[SerializeField] private Player1controls _controls;

    /*
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
    */




    public float speed = 0.5f;

    private Rigidbody2D rb2d;
    private int count, size;

    private bool facingRight;


    public GameObject liteToRight, liteToLeft;
    Vector2 litePos;
    float nextFire = 0.0f;
    public bool playerCanFire;

    /*
    public string up;
    public string down;
    public string left;
    public string right;

    public int player1Number =0;
    public int player2Number =1;
    public int player3Number =2;
    public int player4Number =3;
    */

    weapon weapon;
    public float healingAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        char0 = GameObject.Find("char0");
        char1 = GameObject.Find("char1");
        char2 = GameObject.Find("char2");
        weapon = GetComponent<weapon>();


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

        tutorialText = GameObject.Find("Tutorial Text").GetComponent<tutorialTexts>();

        if (Input.GetJoystickNames().Length > 0)
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                Debug.Log(Input.GetJoystickNames()[i]);

                /*
                up = Input.GetJoystickNames()[i] + " button up";
                Debug.Log(up);
                down = Input.GetJoystickNames()[i] + " button down";
                left = Input.GetJoystickNames()[i] + " button left";
                right = Input.GetJoystickNames()[i] + " button right";
                */
            }
        }

        //else
        //{
        //    up = "W";
        //    down = "S";
        //    left = "A";
        //    right = "D";
        //}

    }

    void FixedUpdate()
    {
        //float horMove = Input.GetAxis("Horizontal");
        //float verMove = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector2(horMove, verMove);
        //rb2d.AddForce(movement*speed);

        //7. Instead of adding force (like above) we use -/+ position to make more responsive movement while also implementing the "speed" variable

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

        //

        playerTouchingEnemy.Invoke();

        //if (Input.GetAxis("Joy" + i + "X") && !isDrummerStationary)
        //{
        //    transform.Translate(horizontalAxis,0,0);
        //    //transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
        //    //facingRight = false;
        //}
        //if (Input.GetAxis("Joy" + i + "Y") && !isDrummerStationary)
        //{
        //    transform.Translate(0, verticalAxis, 0);
        //    //transform.eulerAngles = new Vector3(0, 0, 0); // Normal
        //    //facingRight = true;
        //}
        //if (Input.GetButton("J1a") || Input.GetButton("J1b"))
        //{
        //    Debug.Log("button");
        //}
        //if (Input.GetKey(up) && !isDrummerStationary)
        //{
        //    transform.position += Vector3.up * speed * Time.deltaTime;
        //}
        //if (Input.GetKey(down) && !isDrummerStationary)
        //{
        //    transform.position += Vector3.down * speed * Time.deltaTime;
        //}

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2)
            {
                tutorialText.objComp_HasMoved = true;
                float horizontalAxis = Input.GetAxis("Joy" + i + "X") * speed *0.5f;

                //charID = "char" + i;

                if (i == 0)
                {
                    char0.transform.Translate(horizontalAxis, 0, 0);
                }
                if (i == 1 && !isDrummerStationary)
                {
                    char1.transform.Translate(horizontalAxis, 0, 0);
                }
                if (i == 2)
                {
                    char2.transform.Translate(horizontalAxis, 0, 0);
                }
                //Debug.Log("controller number " + i);
                if (horizontalAxis > 0)
                {
                    //transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
                }
                if (horizontalAxis < 0)
                {
                    //transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                }


                float debugXaxis = Input.GetAxis("Joy" + i + "X");

                Debug.Log(Input.GetJoystickNames()[i] + " is moved on X axis for: " + debugXaxis);
            }
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2)
            {
                tutorialText.objComp_HasMoved = true;

                float verticalAxis = Input.GetAxis("Joy" + i + "Y") * speed * 0.5f;
                if (i == 0)
                {
                    char0.transform.Translate(0, -verticalAxis, 0);
                }
                if (i == 1 && !isDrummerStationary)
                {
                    char1.transform.Translate(0, -verticalAxis, 0);
                }
                if (i == 2)
                {
                    char2.transform.Translate(0, -verticalAxis, 0);
                }

                float debugYaxis = Input.GetAxis("Joy" + i + "Y");

                Debug.Log(Input.GetJoystickNames()[i] + " is moved on y axis for: " + debugYaxis);
            }
            if (Input.GetButtonDown("J" + i + "a"))
            {
                if(i == 0)  //players can fire each other's weapons
                {
                    weaponCanShoot(playerCanFire);
                }
                if (i == 2 && isDrummerStationary)
                {
                    weaponCanShoot(playerCanFire);
                }
                if (i == 3)
                {
                    weaponCanShoot(playerCanFire);
                }

                bool debA = Input.GetButtonDown("J" + i + "a");
                Debug.Log(Input.GetJoystickNames()[i] + " has pressed button: " + debA);
            }
            if (Input.GetButtonDown("J" + i + "b"))
            {
                bool debB = Input.GetButtonDown("J" + i + "b");
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
                //Debug.Log(Input.GetJoystickNames()[i] + " has pressed button: " + debB);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Healing")
        {
            playerHealth += healingAmount ;
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        //Debug.Log(isDrummerStationary);
    }
    public void weaponCanShoot(bool pf)
    {
        weapon.canFire = pf;
        if(pf)
            tutorialText.objComp_FiredNote = true;
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

    void setControllerNumber (int number)
    {
        //horizontalAxis = "J" + number + "Horizontal";
    }

}
