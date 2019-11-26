using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text gui_combo;
    public Text gui_mult;
    public static UnityEvent onBeatEvent;
    public static UnityEvent redButtonCanPressEvent;
    List<PlayerControl> playerList;
    //public noteShooter NoteShooterScript;
    //other references
    public GameObject pause_menu;
    public GameObject red_button;
    buttonController buttonControllerScript;
    boosAnimationScript boss1;
    boss2 boss2;

    public bool game_paused;
    public bool loss;

    //VOTES
    public int vote_heal = 0, vote_damage = 0;
    public double mult_heal = 1, mult_damage = 1;

    bool healBuff = false;
    bool speedBuff = false;
    bool beatBuff = false;
    bool boss1Debuff = false;
    bool boss2Debuff = false;

    public int streak = 0;
    public double combo_mult;
    public double minPlayerHP = 100;

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
        setPaused(game_paused);

        onBeatEvent = new UnityEvent();
        redButtonCanPressEvent = new UnityEvent();
        playerList = new List<PlayerControl>();
        if (_instance == null)
        {
            _instance = this;
        }

        //reference initializations
        //pause_bg = GameObject.Find("Pause_BG").GetComponent<Image>();
        if(pause_menu && !game_paused)
            pause_menu.SetActive(false);
        red_button = GameObject.Find("Buttons_Red");
        if (red_button != null)
            buttonControllerScript = red_button.GetComponent<buttonController>();
        GameObject boss1 = GameObject.Find("Boss1");
        GameObject boss2 = GameObject.Find("Boss2");

        if (red_button!=null)
        {
            Text gui_combo = GameObject.Find("Combo").GetComponent<Text>();
            Text gui_mult = GameObject.Find("Multiplier").GetComponent<Text>();
        }
        if (buttonControllerScript) {
            buttonControllerScript.endBeatEvent.AddListener(resetFireRates);
        }
    }

    //public void redButtonCanPress(bool canShoot)
    //{

    //}

    public void RegisterPlayerControl(PlayerControl pc)
    {
        playerList.Add(pc);
    }
    public void deRegisterPlayerControl(PlayerControl pc)
    {
        playerList.Remove(pc);
    }

    void FixedUpdate()
    {
        mult_heal = (1.0 + (double)GameManager.Instance.vote_heal * 0.01);
        mult_damage = (1.0 + (double)GameManager.Instance.vote_damage * 0.01);
        if (buttonControllerScript != null)
        {
            if (buttonControllerScript.redButtonBeingPressed)
            {
                foreach (PlayerControl pc in playerList)
                {
                    if(pc.GetComponent<weapon>().timeSinceAttackReq<0.2 && !pc.GetComponent<weapon>().hasFired)
                        pc.GetComponent<weapon>().fire();
                }
            }
        }

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
            }
            else if (healBuff)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.healingAmount = 10;
                    healBuff = false;
                    Debug.Log(pc.healingAmount);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.F2))  //Character have increased speed
        {
            if (!speedBuff)
            {
                foreach (PlayerControl pc in playerList)
                {
                    pc.speed = 0.75f;
                    speedBuff = true;
                    Debug.Log(pc.speed);
                }
            }
            else if (speedBuff)
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
            }
            else if (boss1Debuff)
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
            }
            else if (boss2Debuff)
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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (!GameManager.Instance.game_paused)
            {
                GameManager.Instance.game_paused = true;
                setPaused(GameManager.Instance.game_paused);
            }
            else if (GameManager.Instance.game_paused)
            {
                GameManager.Instance.game_paused = false;
                setPaused(GameManager.Instance.game_paused);
            }
        }
        if (gui_combo && gui_mult)
        {
            gui_combo.text = "Combo: " + streak.ToString();
            gui_mult.text = "x" + combo_mult.ToString();
        }
        //findMinRec(playerList, getNumPlayers());
        foreach (PlayerControl pc in playerList)
        {
            if (pc.playerHealth < minPlayerHP || minPlayerHP<0)
                minPlayerHP = pc.playerHealth;
        }
        //print(minPlayerHP);
    }
    void setPaused(bool paused)
    {
        if(paused)
            Time.timeScale = 0f;
        else if(!paused)
            Time.timeScale = 1;
        if (pause_menu)
            pause_menu.SetActive(paused);
    }
    private int getNumPlayers()
    {
        int numPlayers = 0;
        foreach (PlayerControl pc in playerList)
            numPlayers += 1;
        return numPlayers;
    }
    public void resetFireRates() {
        foreach (PlayerControl pc in playerList)
        {
            pc.GetComponent<weapon>().hasFired = false;
            pc.GetComponent<weapon>().hasRequestedFire = false;
        }
    }
    /*
        public static int findMinRec(List<PlayerControl> A, int n)
        {
            // if size = 0 means whole array  
            // has been traversed  
            if (n == 1)
                return A[0];

            return Math.Min(A[n - 1], findMinRec(A, n - 1));
        }
    */
}
