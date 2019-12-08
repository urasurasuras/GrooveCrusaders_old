using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class buttonController : MonoBehaviour
{
    public UnityEvent endBeatEvent;      //resets weapon has fired
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public GameObject flashObject;
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

    [SerializeField]
    [FMODUnity.EventRef]
    string note_miss_sfx ="";
    //public bool playerFireReset;

    // Start is called before the first frame update
    void Start()
    {
        endBeatEvent.AddListener(endBeat);
        //reference to each object's script
        char0 = GameObject.Find("char0").GetComponent<PlayerControl>();
        char1 = GameObject.Find("char1").GetComponent<PlayerControl>();
        char2 = GameObject.Find("char2").GetComponent<PlayerControl>();

        weaponGuitar = char0.GetComponent<weapon>();    //store guitarist weapon
        weaponDrumset = char1.GetComponent<weapon>();   //store drummer weapon
        weaponBass = char2.GetComponent<weapon>();      //store bassist weapon

        redButtonBeingPressed = false;
        theSR = GetComponent<SpriteRenderer>();
        
        flashObject = GameObject.Find("Beat Flash");
        if (flashObject != null)
        {
            flashSripte = flashObject.GetComponent<SpriteRenderer>();
            flashSripte.color = Color.red;
            flashSripte.enabled = false;
        }
        //Debug.Log(flashSripte);
    }

    void Update()
    {
        GameManager.Instance.combo_mult = (1.0 + (double)GameManager.Instance.streak * 0.1);

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
                GameManager.Instance.streak+=1;
                flashSripte.enabled = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            theSR.sprite = defaultImage;
            redButtonBeingPressed = false;
        }

        //Debug.Log("Time since last attack request " + char0lastAtt);


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Note" && flashSripte)
        {
            flashSripte.enabled = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Note" && flashSripte)
        {
            flashSripte.enabled = false;
            endBeatEvent.Invoke();
        }
    }

    private void endBeat()
    {
        GameManager.Instance.streak = 0;
        FMODUnity.RuntimeManager.PlayOneShot(note_miss_sfx,gameObject.transform.position);
    }
    //private void setBoolBack()
    //{
    //    redButtonBeingPressed = false;
    //}

    //public void makePlayersShoot()
    //{
    //    if (char0lastAtt < noteLength )
    //    {
    //        //Debug.Log("Time since last attack request " + char0lastAtt);
    //        //Debug.Log("Note length " + noteLength);
    //        //FIX ME
    //        weaponGuitar.canFire =true;
    //        weaponDrumset.canFire =true;
    //        weaponBass.canFire =true;
    //    }
    //}
}
