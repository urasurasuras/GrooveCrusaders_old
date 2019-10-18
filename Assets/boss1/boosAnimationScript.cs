using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boosAnimationScript : MonoBehaviour
{
    [SerializeField] Animator bossAnimationController;
    public BoxCollider2D centerCollider;
    public BoxCollider2D rightSideCollider;
    public BoxCollider2D leftSideCollider;
    public BoxCollider2D finalPhaseCollider;

    public GameObject enemyAdd;
    public int boss1Health = 100;
    public Slider Boss1HealthSlider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Projectile")
        {
            if(boss1Health < 100)
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

            boss1Health -= 10;

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
        Instantiate(enemyAdd, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        Boss1HealthSlider.value = boss1Health;
        
    }
}
