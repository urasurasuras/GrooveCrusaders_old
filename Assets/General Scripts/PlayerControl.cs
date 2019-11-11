using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public buttonController ButtonController;

    string charID;

    tutorialTexts tutorialText;
    public bool playerFiredTut;
    public bool playerHitTut;

    public float timeSinceAttackReq = 0;
    public float attackCheck = 0.2f;

    public float speed = 0.5f;

    private Rigidbody2D rb2d;

    //private bool facingRight;

    
    public GameObject liteToRight, liteToLeft;
    Vector2 litePos;
    //float nextFire = 0.0f;
    public bool playerCanFire;

    weapon weapon;
    GameObject red_button;
    public float healingAmount = 10;
    private float horizontalAxis;
    private float verticalAxis;

    // Start is called before the first frame update
    void Start()
    {
        char0 = GameObject.Find("char0");
        //char0.SetActive(false);

        char1 = GameObject.Find("char1");
        ///char1.SetActive(false);

        char2 = GameObject.Find("char2");
        //char2.SetActive(false);

        weapon = GetComponent<weapon>();

        red_button = GameObject.Find("Buttons_Red");
        if (red_button != null)
            ButtonController = red_button.GetComponent<buttonController>();
        playerTouchingEnemy = new UnityEvent();
        GameManager.Instance.RegisterPlayerControl(this);
        gameStarted = false;
        rb2d = GetComponent<Rigidbody2D>();
        //facingRight = true;
     
        isDrummerStationary = false;
        drummerAnimationController = GetComponent<Animator>();


        //tutorialText = GameObject.Find("Tutorial Text").GetComponent<tutorialTexts>();
    }

    void FixedUpdate()
    {
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

        if (Input.GetJoystickNames().Length == 0)       //if there are no joysticks connected use WASD for char0
        {
            //Debug.Log("number of joysticks" + Input.GetJoystickNames().Length);
            horizontalAxis = Input.GetAxis("kyb_horizontal") * speed * 0.5f;
            float verticalAxis = Input.GetAxis("kyb_vertical") * speed * 0.5f;
            if (char0 != null)
            {
                if (horizontalAxis < 0)
                {
                    char0.transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                    char0.transform.Translate(-horizontalAxis, 0, 0);
                    Debug.Log("Horizontal axis when less than 0: " + horizontalAxis);
                }
                else if (horizontalAxis > 0)
                {
                    char0.transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
                    char0.transform.Translate(horizontalAxis, 0, 0);
                    Debug.Log("Horizontal axis when more than 0: " + horizontalAxis);

                }
                char0.transform.Translate(0, verticalAxis, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    weaponCanShoot(playerCanFire);
                }
            }
        }
        else//if (Input.GetJoystickNames().Length > 0)
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2)
                {
                    //tutorialText.objComp_HasMoved = true;
                    horizontalAxis = Input.GetAxis("Joy" + i + "X") * speed * 0.5f;

                    //charID = "char" + i;

                    if (i == 0 && char0 != null)
                    {
                        if (horizontalAxis < 0)
                        {
                            char0.transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                            char0.transform.Translate(-horizontalAxis, 0, 0);
                            Debug.Log("Horizontal axis when less than 0: " + horizontalAxis);
                        }
                        else if (horizontalAxis > 0)
                        {
                            char0.transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
                            char0.transform.Translate(horizontalAxis, 0, 0);
                            Debug.Log("Horizontal axis when more than 0: " + horizontalAxis);

                        }
                    }
                    if (i == 1 && char1 != null && !isDrummerStationary)
                    {
                        if (horizontalAxis < 0)
                        {
                            char1.transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                            char1.transform.Translate(-horizontalAxis, 0, 0);
                            Debug.Log("Horizontal axis when less than 0: " + horizontalAxis);
                        }
                        else if (horizontalAxis > 0)
                        {
                            char1.transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
                            char1.transform.Translate(horizontalAxis, 0, 0);
                            Debug.Log("Horizontal axis when more than 0: " + horizontalAxis);

                        }
                    }
                    if (i == 2 && char2 != null)
                    {
                        if (horizontalAxis < 0)
                        {
                            char2.transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                            char2.transform.Translate(-horizontalAxis, 0, 0);
                            Debug.Log("Horizontal axis when less than 0: " + horizontalAxis);
                        }
                        else if (horizontalAxis > 0)
                        {
                            char2.transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
                            char2.transform.Translate(horizontalAxis, 0, 0);
                            Debug.Log("Horizontal axis when more than 0: " + horizontalAxis);

                        }
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

                    //Debug.Log(Input.GetJoystickNames()[i] + " is moved on X axis for: " + debugXaxis);
                }
                if (Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2)
                {
                    //tutorialText.objComp_HasMoved = true;

                    verticalAxis = Input.GetAxis("Joy" + i + "Y") * speed * 0.5f;
                    if (i == 0 && char0 != null)
                    {
                        char0.transform.Translate(0, -verticalAxis, 0);
                    }
                    if (i == 1 && char1 != null && !isDrummerStationary)
                    {
                        char1.transform.Translate(0, -verticalAxis, 0);
                    }
                    if (i == 2 && char2 != null)
                    {
                        char2.transform.Translate(0, -verticalAxis, 0);
                    }

                    float debugYaxis = Input.GetAxis("Joy" + i + "Y");

                    //Debug.Log(Input.GetJoystickNames()[i] + " is moved on y axis for: " + debugYaxis);
                }
                timeSinceAttackReq += Time.deltaTime;
                if (Input.GetButtonDown("J" + i + "a"))
                {

                    if (i == 0 && char0 != null)  //players can fire each other's weapons
                    {
                        weaponCanShoot(playerCanFire);
                    }
                    if (i == 1 && char1 != null && isDrummerStationary)
                    {
                        weaponCanShoot(playerCanFire);
                    }
                    if (i == 2 && char2 != null)
                    {
                        weaponCanShoot(playerCanFire);
                    }
                    //ButtonController.makePlayersShoot();

                    timeSinceAttackReq = 0;


                    bool debA = Input.GetButtonDown("J" + i + "a");
                    //Debug.Log(Input.GetJoystickNames()[i] + " has pressed button: " + debA);
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "f_healing")
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
 
        if (Input.GetJoystickNames().Length > 0)
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                

                //Debug.Log(Input.GetJoystickNames()[i]);

                /*
                up = Input.GetJoystickNames()[i] + " button up";
                Debug.Log(up);
                down = Input.GetJoystickNames()[i] + " button down";
                left = Input.GetJoystickNames()[i] + " button left";
                right = Input.GetJoystickNames()[i] + " button right";
                */
            }
        }
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
        drummerAnimationController.SetBool("isStationary", false);
        isDrummerStationary = false;
    }
    public void weaponCanShoot(bool pf)
    {
        weapon.canFire = pf;

        //for tut
        if(pf && tutorialText!=null)
            tutorialText.objComp_FiredNote = true;
        //
    }
    private void dmgOverTime()
    {
        playerHealth -= 0.002;
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