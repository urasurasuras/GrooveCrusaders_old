using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public SpriteRenderer flashSripte;

    public PlayerControl char0;
    public PlayerControl char1;
    public PlayerControl char2;

    public bool noteCanBePressed;
    public bool redButtonCanBePressed;
    public bool redButtonBeingPressed;

    public static buttonController instance;
    noteScroll noteScript;

    public KeyCode keyToPress;

    public float timeSinceLastNoteHit = 0;
    public float noteLength = 0.2f;

    weapon weaponGuitar;
    weapon weaponDrumset;
    weapon weaponBass;
    private float char0lastAtt;

    // Start is called before the first frame update
    void Start()
    {
        //reference to each object's script
        char0 = GameObject.Find("char0").GetComponent<PlayerControl>();
        char1 = GameObject.Find("char1").GetComponent<PlayerControl>();
        char2 = GameObject.Find("char2").GetComponent<PlayerControl>();

        weaponGuitar = char0.GetComponent<weapon>();    //store guitarist weapon
        weaponDrumset = char1.GetComponent<weapon>();   //store drummer weapon
        weaponBass = char2.GetComponent<weapon>();      //store bassist weapon

        redButtonBeingPressed = false;
        theSR = GetComponent<SpriteRenderer>();

        flashSripte = GameObject.Find("Beat Flash").GetComponent<SpriteRenderer>();
        flashSripte.color = Color.red;
        flashSripte.enabled = false;
        Debug.Log(flashSripte);
    }

    void Update()
    {
        timeSinceLastNoteHit += Time.deltaTime;
        
        

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            theSR.sprite = pressedImage;

            if (noteCanBePressed)
            {
                redButtonBeingPressed = true;
                //Debug.Log("note can be pressed: "+noteCanBePressed);
                //Invoke("setBoolBack", noteLength);
            }
            if (flashSripte.enabled)
            {
                timeSinceLastNoteHit = 0;
                makePlayersShoot();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            theSR.sprite = defaultImage;
            redButtonBeingPressed = false;
        }
        GameManager.Instance.redButtonCanPress(redButtonBeingPressed);

        char0lastAtt = char0.timeSinceAttackReq;
        //Debug.Log("Time since last attack request " + char0lastAtt);


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

    public void makePlayersShoot()
    {
        if (char0lastAtt < noteLength )
        {
            //Debug.Log("Time since last attack request " + char0lastAtt);
            //Debug.Log("Note length " + noteLength);
            //FIX ME
            weaponGuitar.canFire =true;
            weaponDrumset.canFire =true;
            weaponBass.canFire =true;
        }
    }
}
