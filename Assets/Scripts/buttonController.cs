﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public bool redButtonCanBePressed;
    public bool redButtonBeingPressed;

    public static buttonController instance;


    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        redButtonBeingPressed = false;
        theSR = GetComponent<SpriteRenderer>();
        //GameManager.Instance.registerRedButton(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) /*&& redButtonCanBePressed*/)
        {
            redButtonBeingPressed = true;
            Debug.Log(redButtonBeingPressed);
            Invoke("setBoolBack",0.5f);
            theSR.sprite = pressedImage;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            redButtonBeingPressed = false;
            theSR.sprite = defaultImage;
        }

    }
    private void setBoolBack()
    {
        redButtonBeingPressed = false;
    }

    
}
