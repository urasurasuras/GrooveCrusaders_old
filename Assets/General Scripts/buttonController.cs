using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public SpriteRenderer flashSripte;

    public bool noteCanBePressed;
    public bool redButtonCanBePressed;
    public bool redButtonBeingPressed;

    public float noteLength = 0.5f;

    public static buttonController instance;
    noteScroll noteScript;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        redButtonBeingPressed = false;
        theSR = GetComponent<SpriteRenderer>();

        //flash = GameObject.Find("Beat Flash");
        //flash.SetActive(false);
        flashSripte = GameObject.Find("Beat Flash").GetComponent<SpriteRenderer>();
        flashSripte.color = Color.red;
        flashSripte.enabled = false;
        Debug.Log(flashSripte);
        //GameManager.Instance.registerRedButton(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            theSR.sprite = pressedImage;

            if (noteCanBePressed)
            {
                redButtonBeingPressed = true;
                //Debug.Log("note can be pressed: "+noteCanBePressed);
                //Invoke("setBoolBack", noteLength);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //redButtonBeingPressed = false;
            theSR.sprite = defaultImage;
            redButtonBeingPressed = false;
        }
        GameManager.Instance.redButtonCanPress(redButtonBeingPressed);
        //if(!redButtonBeingPressed)
        //    GameManager.Instance.redButtonCanPress(false);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            flashSripte.enabled = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note")
        {
            flashSripte.enabled = false;
        }
    }
    private void setBoolBack()
    {
        redButtonBeingPressed = false;
    }
 }
