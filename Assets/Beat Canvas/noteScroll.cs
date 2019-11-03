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

    buttonController buttonControllerScript;
    public int lifeTime = 5;

    void Awake() { Destroy(gameObject, lifeTime); }
    // Start is called before the first frame update
    void Start()
    {
        buttonControllerScript = GameObject.Find("Buttons_Red").GetComponent<buttonController>();
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
        transform.position -= new Vector3(notespeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            currentNote = gameObject;
            buttonControllerScript.noteCanBePressed = true;
            //Debug.Log(noteCanBePressed);
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
