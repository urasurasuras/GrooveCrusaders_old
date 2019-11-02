﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialTexts : MonoBehaviour
{
    public Text tutText;

    Animator textAnimator;

    buttonController buttonControllerScript;
    //PlayerControl playerControlScript; 

    public GameObject enemyAdd;


    public bool objComp_pressedRedButton;
    public bool objComp_FiredNote;
    public bool objComp_HitEnemy;
    public bool objComp_END;
    private bool isCreated;
    public bool objComp_HasMoved;

    // Start is called before the first frame update
    void Start()
    {
        buttonControllerScript = GameObject.Find("Buttons_Red").GetComponent<buttonController>();
        //playerControlScript = GameObject.Find("char0").GetComponent<PlayerControl>();

        textAnimator = GetComponent<Animator>();

        objComp_pressedRedButton = false;
        objComp_FiredNote = false;
        objComp_HitEnemy = false;
        objComp_END = false;
        //buttonControllerScript = GameObject.Find("Buttons_Red").GetComponent<buttonController>();
        tutText.text= "Conductor, press Space on beat to activate the red button.";
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonControllerScript.redButtonBeingPressed)
            objComp_pressedRedButton = true;
        if (objComp_END)
        {
            tutText.text = "Good job band, now GROOVE !";
        }
        else if (objComp_HitEnemy)
        {
            tutText.text = "Good job conductor, now musicians fire your weapons !";
        }
        /*else if (objComp_HasMoved)
        {
            tutText.text = "Oh yeah, also move using your left stick.";
        }*/
        else if (objComp_FiredNote)
        {
            tutText.text = "Good job musicians, now hit the enemies !";
            if (!isCreated)
            {
                Instantiate(enemyAdd);
                isCreated = true;
            }
        }
        else if (objComp_pressedRedButton)
        {
            tutText.text = "Good job conductor, now musicians fire your weapons\n using the A button on your controllers !";
        }
    }
}
