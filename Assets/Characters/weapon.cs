using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject attackPrefab;   //projectile prefab
    GameObject attack;                //Current attack GameObject reference
    public Transform firePoint;     //position from where to fire 
    public bool canFire;

    // Start is called before the first frame update
    protected void Start()
    {
       
    }

    // Update is called once per frame
    protected void Update()
    {
        if (/*Input.GetKeyDown(KeyCode.Space) && */canFire)
        {
            fire();
            canFire = false;
        }
    }
    protected void fire()
    {
        //Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        attack = Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        if (attack.GetComponent<projectile>())
        {
            attack.GetComponent<projectile>().owner = gameObject;
        }
        else if (attack.GetComponent<drummerPulse>())
        {
            attack.GetComponent<drummerPulse>().owner = gameObject;
        }
        //projectileScript.owner = gameObject;
        //print(projectileScript.owner);
    }
}
