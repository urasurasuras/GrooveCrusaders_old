﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class noteScroll : MonoBehaviour
{
    public float notespeed;
    public bool noteCanBePressed;
    public KeyCode keyToPress;

    public int lifeTime = 5;

    void Awake() { Destroy(gameObject, lifeTime); }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (noteCanBePressed)
            {
                noteShooter.instance.noteHit();
                gameObject.SetActive(false);
            }
        }
        transform.position -= new Vector3(notespeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            //Debug.Log("can be pressed");
            noteCanBePressed = true;
            GameManager.Instance.redButtonCanPress(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            noteCanBePressed = false;
            GameManager.Instance.redButtonCanPress(false);
        }
    }
    public bool learnIfCanAttack()
    {
        return noteCanBePressed;
    }
}
