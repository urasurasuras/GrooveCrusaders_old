﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boosAnimationScript : MonoBehaviour
{
    Animator bossAnimationController;
    public BoxCollider2D centerCollider;
    //public BoxCollider2D rightSideCollider;
    //public BoxCollider2D leftSideCollider;
    //public BoxCollider2D finalPhaseCollider;

    public GameObject enemyAdd;
    public float boss1Health = 100;
    public float maxHealth = 200;
    public float stage2health = 50;
    public Slider Boss1HealthSlider;

    public float dmgTaken = 10; //CHANGE THIS TO BE A MULTIPLIER
    public float percentage;    //from 0-1

    void Start()
    {
        noteShooter.barEvent.AddListener(spawnEnemyAdd);
        bossAnimationController = GetComponent<Animator>();
        centerCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bossAnimationController.SetTrigger("RightFight");

        if (other.gameObject.tag =="f_damage")
        {
            boss1Health -= (float)other.gameObject.GetComponent<projectile>().value_final;   
        }else if (other.gameObject.tag == "f_healing")
        {
            boss1Health -= (float)(other.gameObject.GetComponent<projectile>().value_final  /   5);//Healer does damage to boss
        }
    }
    //public void activateCenter()
    //{
    //    centerCollider.enabled = true;
    //    rightSideCollider.enabled = false;
    //    leftSideCollider.enabled = false;
    //    finalPhaseCollider.enabled = false;
    //}
    public void activateRight()
    {
    //    rightSideCollider.enabled = true;
    //    centerCollider.enabled = false;
    //    leftSideCollider.enabled = false;
    //    finalPhaseCollider.enabled = false;
    }
    //public void activateLeft()
    //{
    //    rightSideCollider.enabled = true;
    //    centerCollider.enabled = false;
    //    leftSideCollider.enabled = false;
    //    finalPhaseCollider.enabled = false;
    //}
    public void activateFinal()
    {
    //    finalPhaseCollider.enabled = true;
    //    rightSideCollider.enabled = false;
    //    centerCollider.enabled = false;
    //    leftSideCollider.enabled = false;
    }
    //public void onbossdefeat()
    //{
    //    finalphasecollider.enabled = false;
    //    rightsidecollider.enabled = false;
    //    centercollider.enabled = false;
    //    leftsidecollider.enabled = false;
    //}
    // Start is called before the first frame update
    
    void spawnEnemyAdd()
    {
        Instantiate(enemyAdd, new Vector3(0, 0, 0),Quaternion.identity);
        //print("spawned: " + enemyAdd);
    }
    // Update is called once per frame
    void Update()
    {
        Boss1HealthSlider.value = boss1Health;
        percentage = boss1Health / maxHealth;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, percentage);
        //Debug.Log(this + " hit by "+other);
        if (boss1Health < maxHealth)
        {
            bossAnimationController.SetTrigger("RightFight");
            //activateRight();
        }
        if (boss1Health <= stage2health)
        {
            bossAnimationController.SetTrigger("FinalFight");
            //activateFinal();
        }
        if (boss1Health <= 0)
        {
            bossAnimationController.SetTrigger("BossDefeat");
            centerCollider.enabled = false;
            //onBossDefeat();
        }
    }
}
