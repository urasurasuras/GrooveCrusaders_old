using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{

    public static UnityEvent playerTouchingEnemy;
    public float maxHealth;
    public float playerHealth;
    public bool gameStarted;
    Animator drummerAnimationController;

    healingLite healingProj;

    public bool isDrummerStationary;

    //public GameObject currentChar;
    public int controllerNum;
    float contDeadz = .2f;

    public Image playerHealthBar;

    public buttonController ButtonController;

    string charID;

    tutorialTexts tutorialText;
    public bool playerFiredTut;
    public bool playerHitTut;

    public float timeSinceAttackReq = 0;
    public float attackCheck = 0.2f;

    public float speed = 0.5f;

    private Rigidbody2D rb2d;

    public bool facingRight = true;

    
    public GameObject liteToRight, liteToLeft;
    Vector2 litePos;
    //float nextFire = 0.0f;
    public bool playerCanFire;

    weapon weapon;
    GameObject red_button;
    public float healingAmount = 10;
    public float horizontalAxis;
    public float verticalAxis;

    // Start is called before the first frame update
    void Start()
    {
        //currentChar = gameObject;
        playerHealth = maxHealth;
        //char0 = GameObject.Find("char0");
        //char0.SetActive(false);

        //char1 = GameObject.Find("char1");
        ///char1.SetActive(false);

        //char2 = GameObject.Find("char2");
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
            Destroy(gameObject,0f);
        }

        //

        playerTouchingEnemy.Invoke();



        if (Input.GetJoystickNames().Length == 0)       //if there are no joysticks connected use WASD for char0
        {
            //Debug.Log("number of joysticks" + Input.GetJoystickNames().Length);
            horizontalAxis = Input.GetAxis("kyb_horizontal") * speed * 0.5f;
            float verticalAxis = Input.GetAxis("kyb_vertical") * speed * 0.5f;
            
                if (horizontalAxis < 0)             //facing left
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    transform.Translate(-horizontalAxis, 0, 0);
                    facingRight = false;
                    //Debug.Log("Horizontal axis when less than 0: " + horizontalAxis);
                }
                else if (horizontalAxis > 0)        //facing right
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    transform.Translate(horizontalAxis, 0, 0);
                    facingRight = true;
                    //Debug.Log("Horizontal axis when more than 0: " + horizontalAxis);

                }
                transform.Translate(0, verticalAxis, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    weaponCanShoot(playerCanFire);
                }
            
        }


        else//if (Input.GetJoystickNames().Length > 0)
        {
            //for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            //{
                if (Mathf.Abs(Input.GetAxis("Joy" + controllerNum + "X")) > contDeadz)
                {
                    //tutorialText.objComp_HasMoved = true;
                    horizontalAxis = Input.GetAxis("Joy" + controllerNum + "X") * speed * 0.5f;

                        if (horizontalAxis < 0)         //facing left
                        {
                            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
                            transform.Translate(-horizontalAxis, 0, 0);
                            facingRight = false;
                            //Debug.Log("Horizontal axis when less than 0: " + horizontalAxis);
                        }
                        else if (horizontalAxis > 0)    //facing right
                        {
                            transform.eulerAngles = new Vector3(0, 0, 0); // Flipped
                            transform.Translate(horizontalAxis, 0, 0);
                            facingRight = true;
                            //Debug.Log("Horizontal axis when more than 0: " + horizontalAxis);
                        }
                    
                    //Debug.Log(Input.GetJoystickNames()[i] + " is moved on X axis for: " + debugXaxis);
                }
                if (Mathf.Abs(Input.GetAxis("Joy" + controllerNum + "Y")) > contDeadz)
                {
                    //tutorialText.objComp_HasMoved = true;

                    verticalAxis = Input.GetAxis("Joy" + controllerNum + "Y") * speed * 0.5f;
                    transform.Translate(0, -verticalAxis, 0);
                    
                    //Debug.Log(Input.GetJoystickNames()[i] + " is moved on y axis for: " + debugYaxis);
                }
                timeSinceAttackReq += Time.deltaTime;
                if (Input.GetButtonDown("J" + controllerNum + "a"))
                {
                    weaponCanShoot(playerCanFire);
                    timeSinceAttackReq = 0;
                    //Debug.Log(Input.GetJoystickNames()[i] + " has pressed button: " + debA);
                }
                if (Input.GetButtonDown("J" + controllerNum + "b") /*|| Input.GetKey(KeyCode.Keypad0)*/)
                {
                    bool debB = Input.GetButtonDown("J" + controllerNum + "b");
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
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "f_healing" && playerHealth <maxHealth)
        {
            playerHealth += healingAmount ;
            //Destroy(other.gameObject);
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
        playerHealthBar.fillAmount = playerHealth/maxHealth;
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
        if (drummerAnimationController)
        {
            drummerAnimationController.SetBool("isStationary", true);
            isDrummerStationary = true;
            Debug.Log(isDrummerStationary);
        }
    }
    public void setDrummerWalking()
    {
        if (drummerAnimationController)
        {
            drummerAnimationController.SetBool("isStationary", false);
            isDrummerStationary = false;
        }
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
        playerHealth -= 0.0015f;
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