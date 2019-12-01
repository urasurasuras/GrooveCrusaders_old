using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class noteScroll : MonoBehaviour
{
    public float notespeed;
    public bool noteCanBePressed;
    public KeyCode keyToPress;

    private static GameObject currentNote;

    GameObject red_button;
    buttonController buttonControllerScript;
    public int lifeTime = 5;

    Vector3 direction;
    float distance;
    float travel_time;
    void Awake() { Destroy(gameObject, lifeTime); }
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Buttons_Red"))
        {
            red_button = GameObject.Find("Buttons_Red");
            buttonControllerScript = red_button.GetComponent<buttonController>();
            direction = red_button.GetComponent<Transform>().position - transform.position;
            distance = direction.magnitude;
            travel_time = 60/(float)noteShooter.bpm;//seconds
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (buttonControllerScript.noteCanBePressed)
            {
                noteShooter.instance.noteHit();
                if (currentNote == gameObject)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    void Update()
    {
        //print(direction +"-"+ travel_time +"-"+distance);
        //transform.Translate(-distance / travel_time,0,0);
        transform.position += new Vector3(-distance/travel_time*Time.deltaTime/2,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            currentNote = gameObject;
            buttonControllerScript.noteCanBePressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            buttonControllerScript.noteCanBePressed = false;
        }
    }
    public bool learnIfCanAttack()
    {
        return noteCanBePressed;
    }
}
