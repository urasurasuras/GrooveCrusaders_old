using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{

    public static UnityEvent playerTouchingEnemy;

    //Char values
    GameObject currChar;
    weapon currWeapon;
    public float maxHealth;
    public float playerHealth;
    public float speed;
    public bool facingRight = true;
    public double power;    //current power of player, passed onto their attacks

    Animator drummerAnimationController;
    public GameObject blood_particle;

    public bool isDrummerStationary;

    //Controller values
    public int controllerNum;
    float contDeadz = .25f;

    public Image playerHealthBar;

    public buttonController ButtonController;

    //Tutorial
    tutorialTexts tutorialText;
    public bool playerFiredTut;
    public bool playerHitTut;

    public float attackCheck = 0.2f;


    private Rigidbody2D rb2d;


    //public GameObject liteToRight, liteToLeft;
    //Vector2 litePos;
    //float nextFire = 0.0f;
    public bool playerCanFire;

    //public GameObject weapon;
    GameObject red_button;
    public float healingAmount = 10;
    public float horizontalAxis;
    public float verticalAxis;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;

        red_button = GameObject.Find("Buttons_Red");
        if (red_button != null)
            ButtonController = red_button.GetComponent<buttonController>();
        playerTouchingEnemy = new UnityEvent();
        GameManager.Instance.RegisterPlayerControl(this);
        rb2d = GetComponent<Rigidbody2D>();
        //facingRight = true;
        speed = ((float)noteShooter.bpm / 100);
        //print(noteShooter.bpm);
        isDrummerStationary = false;
        drummerAnimationController = GetComponent<Animator>();


        //tutorialText = GameObject.Find("Tutorial Text").GetComponent<tutorialTexts>();
    }

    void FixedUpdate()
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject, 0f);
            GameManager.Instance.deRegisterPlayerControl(this);
        }
        playerTouchingEnemy.Invoke();

        if (Input.GetJoystickNames().Length == 0)       //if there are no joysticks connected use WASD for char0
        {
            //Debug.Log("number of joysticks" + Input.GetJoystickNames().Length);
            horizontalAxis = Input.GetAxis("kyb_horizontal") * speed * 0.5f;
            verticalAxis = Input.GetAxis("kyb_vertical") * speed * 0.5f;

            //print(horizontalAxis);
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
                gameObject.GetComponent<weapon>().timeSinceAttackReq = 0;
                gameObject.GetComponent<weapon>().hasRequestedFire = true;
            }
        }


        else//if (Input.GetJoystickNames().Length > 0)
        {
            //for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            //{
            if (Mathf.Abs(Input.GetAxis("Joy" + controllerNum + "X")) > contDeadz)
            {
                print("transforming player " + gameObject.name);
                //tutorialText.objComp_HasMoved = true;
                horizontalAxis = Input.GetAxis("Joy" + controllerNum + "X") * speed * 0.5f;
                if (gameObject.name != "char1")
                {
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
                }
                else if (gameObject.name == "char1" && !isDrummerStationary)
                {
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
                }
                

                //Debug.Log(Input.GetJoystickNames()[i] + " is moved on X axis for: " + debugXaxis);
            }
            if (Mathf.Abs(Input.GetAxis("Joy" + controllerNum + "Y")) > contDeadz)
            {
                //tutorialText.objComp_HasMoved = true;
                if (gameObject.name != "char1")
                {
                    verticalAxis = Input.GetAxis("Joy" + controllerNum + "Y") * speed * 0.5f;
                    transform.Translate(0, -verticalAxis, 0);
                }
                else if (gameObject.name == "char1" && !isDrummerStationary)
                {
                    verticalAxis = Input.GetAxis("Joy" + controllerNum + "Y") * speed * 0.5f;
                    transform.Translate(0, -verticalAxis, 0);
                }
               
                //Debug.Log(Input.GetJoystickNames()[i] + " is moved on y axis for: " + debugYaxis);
            }
            gameObject.GetComponent<weapon>().timeSinceAttackReq += Time.deltaTime;
            if (Input.GetButtonDown("J" + controllerNum + "a"))
            {
                if (gameObject.name != "char1")
                {
                    //print("im not char1");
                    if (ButtonController.timeSinceLastNoteHit < 0.2)
                        gameObject.GetComponent<weapon>().fire();
                    gameObject.GetComponent<weapon>().timeSinceAttackReq = 0;
                    gameObject.GetComponent<weapon>().hasRequestedFire = true;
                }
                else if (gameObject.name == "char1" && isDrummerStationary)
                {
                    if (ButtonController.timeSinceLastNoteHit < 0.2)
                        gameObject.GetComponent<weapon>().fire();
                    gameObject.GetComponent<weapon>().timeSinceAttackReq = 0;
                    gameObject.GetComponent<weapon>().hasRequestedFire = true;
                }
            }
            if (Input.GetButtonDown("J" + controllerNum + "b") && gameObject.name=="char1")
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
        if (other.gameObject.tag == "f_healing" && playerHealth < maxHealth)
        {
            playerHealth += (float)other.gameObject.GetComponent<projectile>().value_final;
            //Destroy(other.gameObject);
        }
    
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(blood_particle, transform.position, Quaternion.identity);
            playerHealth -= 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        speed = ((float)noteShooter.bpm / 500);

        if (power!=null)
        power = GameManager.Instance.combo_mult;//we cache this value for each player individually

        playerHealthBar.fillAmount = playerHealth / maxHealth;
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
    //public void weaponCanShoot(bool pf)
    //{
    //    if (!gameObject.GetComponent<weapon>().hasFired)
    //        gameObject.GetComponent<weapon>().canFire = pf;
    //    //gameObject.GetComponent<weapon>().canFire = false;

    //    //for tut
    //    if(pf && tutorialText!=null)
    //        tutorialText.objComp_FiredNote = true;
    //    //
    //}
    private void dmgOverTime()
    {
        playerHealth -= 0.0015f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("touching boss");
            playerTouchingEnemy.AddListener(dmgOverTime);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("not touching enemy anymore");
            playerTouchingEnemy.RemoveListener(dmgOverTime);
        }
    }

    void setControllerNumber(int number)
    {
        //horizontalAxis = "J" + number + "Horizontal";
    }

}