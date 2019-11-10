using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boosAnimationScript : MonoBehaviour
{
    Animator bossAnimationController;
    public BoxCollider2D centerCollider;
    public BoxCollider2D rightSideCollider;
    public BoxCollider2D leftSideCollider;
    public BoxCollider2D finalPhaseCollider;

    public GameObject enemyAdd;
    public float boss1Health = 100;
    public float maxHealth = 200;
    public Slider Boss1HealthSlider;

    public float dmgTaken = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Projectile")
        {
            Debug.Log(this + " hit by "+other);
            if(boss1Health < maxHealth)
            {
                bossAnimationController.SetTrigger("RightFight");
                activateRight();
            }
            if (boss1Health <= 50)
            {
                bossAnimationController.SetTrigger("FinalFight");
                activateFinal();
            }
            if (boss1Health <= 0)
            {
                bossAnimationController.SetTrigger("BossDefeat");
                onBossDefeat();
            }
            //Debug.Log("boss Hit!!");

            boss1Health -= dmgTaken;

            bossAnimationController.SetTrigger("RightFight");
        }
    }
    public void activateCenter()
    {
        centerCollider.enabled = true;
        rightSideCollider.enabled = false;
        leftSideCollider.enabled = false;
        finalPhaseCollider.enabled = false;
    }
    public void activateRight()
    {
        rightSideCollider.enabled = true;
        centerCollider.enabled = false;
        leftSideCollider.enabled = false;
        finalPhaseCollider.enabled = false;
    }
    public void activateLeft()
    {
        rightSideCollider.enabled = true;
        centerCollider.enabled = false;
        leftSideCollider.enabled = false;
        finalPhaseCollider.enabled = false;
    }
    public void activateFinal()
    {
        finalPhaseCollider.enabled = true;
        rightSideCollider.enabled = false;
        centerCollider.enabled = false;
        leftSideCollider.enabled = false;
    }
    public void onBossDefeat()
    {
        finalPhaseCollider.enabled = false;
        rightSideCollider.enabled = false;
        centerCollider.enabled = false;
        leftSideCollider.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        noteShooter.barEvent.AddListener(spawnEnemyAdd);
        bossAnimationController = GetComponent<Animator>();
    }

    void spawnEnemyAdd()
    {
        Instantiate(enemyAdd);
    }
    // Update is called once per frame
    void Update()
    {
        Boss1HealthSlider.value = boss1Health;
    }
}
