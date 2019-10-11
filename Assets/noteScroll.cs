﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteScroll : MonoBehaviour
{
    public float notespeed;
    public bool canBePressed;
    public KeyCode keyToPress;

    public int lifeTime = 10;

    void Awake() { Destroy(gameObject, lifeTime); }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                noteShooter.instance.noteHit();
            }
        }
        transform.position -= new Vector3(notespeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            Debug.Log("can be pressed");
            canBePressed = true;
            GameManager.Instance.PlayerCanShoot(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = false;

            GameManager.Instance.PlayerCanShoot(false);
        }
    }
    public bool learnIfCanAttack()
    {
        return canBePressed;
    }
}
