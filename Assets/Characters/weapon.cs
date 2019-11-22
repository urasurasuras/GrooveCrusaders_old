using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject attackPrefab;   //projectile prefab
    GameObject attack;                //Current attack GameObject reference
    public Transform firePoint;     //position from where to fire 

    public float timeSinceAttackReq;
    public bool hasRequestedFire=false;

    //public bool canFire=false;
    public bool hasFired=true;

    // Start is called before the first frame update
    protected void Start()
    {
       
    }

    // Update is called once per frame
    protected void Update()
    {
        //if (/*Input.GetKeyDown(KeyCode.Space) && */canFire&&!hasFired)
        //{
        //    fire();
        //    hasFired = true;
        //}
    }
    public void fire()
    {
        attack = Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        if (attack.GetComponent<projectile>())
        {
            attack.GetComponent<projectile>().owner = gameObject;
        }
        else if (attack.GetComponent<drummerPulse>())
        {
            attack.GetComponent<drummerPulse>().owner = gameObject;
        }
        hasFired = true;
    }
}
