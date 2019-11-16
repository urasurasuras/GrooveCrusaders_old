using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static UnityEvent onBeatEvent;
    public static UnityEvent redButtonCanPressEvent;
    List<PlayerControl> playerList;

    //other references
    GameObject red_button;
    buttonController buttonControllerScript;
    boosAnimationScript boss1;
    boss2 boss2;

    public bool loss;

    bool healBuff = false;
    bool speedBuff = false;
    bool beatBuff = false;
    bool boss1Debuff = false;
    bool boss2Debuff = false;

    public int streak=0;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private static GameManager _instance;
    // Start is called before the first frame update
    void Start()
    {
        onBeatEvent = new UnityEvent();
        redButtonCanPressEvent = new UnityEvent();
        playerList = new List<PlayerControl>();
        if (_instance == null)
        {
            _instance = this;
        }
        
        //reference initializations
        red_button = GameObject.Find("Buttons_Red");
        if(red_button!=null)
        buttonControllerScript = red_button.GetComponent<buttonController>();
        GameObject boss1 = GameObject.Find("Boss1");
        GameObject boss2 = GameObject.Find("Boss2");
    }

    //public void redButtonCanPress(bool canShoot)
    //{
        
    //}

    public void RegisterPlayerControl(PlayerControl pc)
    {
        playerList.Add(pc);
    }
   
    void FixedUpdate()
    {
        //buttonControllerScript.redButtonCanBePressed = canShoot;
        //Debug.Log("red button can press: " + canShoot);
        if (buttonControllerScript != null)
        {
            if (buttonControllerScript.timeSinceLastNoteHit < buttonControllerScript.noteLength)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.playerCanFire = true;
                }
            }
            else
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.playerCanFire = false;
                }
            }
        }
        //for (int i = 0; i < playerList.Count; i++)
        //{
        //if (playerList[i].horizontalAxis > 0)
        //    {
        //        playerList[i].facingRight = true;
        //        //print(pc + " is facing right " + pc.facingRight);
        //    }
        //if (playerList[i].horizontalAxis < 0)
        //    {
        //        playerList[i].facingRight = false;
        //        //print(pc + " is facing right " + pc.facingRight);
        //    }
        //}

        redButtonCanPressEvent.Invoke();
        onBeatEvent.Invoke();

        //Modifiers
        if (Input.GetKeyDown(KeyCode.F1))   //Character receive more healing
        {
            if (!healBuff)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.healingAmount = 20;
                    healBuff = true;
                    Debug.Log(pc.healingAmount);
                }
            }else if (healBuff)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.healingAmount = 10;
                    healBuff = false;
                    Debug.Log(pc.healingAmount);
                }
            }
            
        }if (Input.GetKeyDown(KeyCode.F2))  //Character have increased speed
        {
            if (!speedBuff)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.speed = 0.75f;
                    speedBuff = true;
                    Debug.Log(pc.speed);
                }
            }else if (speedBuff)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.speed = 0.5f;
                    speedBuff = false;
                    Debug.Log(pc.speed);
                }
            }
        }
        
        //if (Input.GetKeyDown(KeyCode.F3))  //Characters have more time to fire each beat 
        //{
        //    if (!beatBuff)
        //    {
        //        buttonControllerScript.noteLength = 1;
        //        beatBuff = true;
        //        Debug.Log(buttonControllerScript.noteLength);
        //    }else if (beatBuff)
        //    {
        //        buttonControllerScript.noteLength = .5f;
        //        beatBuff = false;
        //        Debug.Log(buttonControllerScript.noteLength);
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.F4))  //Boss1 takes more damage 
        {
            if (!boss1Debuff)
            {
                boss1.dmgTaken = 15;
                boss1Debuff = true;
                Debug.Log(boss1.dmgTaken);
            }else if (boss1Debuff)
            {
                boss1.dmgTaken = 10;
                boss1Debuff = false;
                Debug.Log(boss1.dmgTaken);
            }

        }
        if (Input.GetKeyDown(KeyCode.F5))  //Boss2 charges slower
        {
            if (!boss2Debuff)
            {
                boss2.speed = 5;
                boss2Debuff = true;
                Debug.Log(boss2.speed);
            }else if (boss2Debuff)
            {
                boss2.speed = 10;
                boss2Debuff = false;
                Debug.Log(boss2.speed);
            }
        }
        //
        //GUI BOX DEBUG
    }
    private void Update()
    {
        //if (loss)
        //    Debug.Log(loss);
    }
}
