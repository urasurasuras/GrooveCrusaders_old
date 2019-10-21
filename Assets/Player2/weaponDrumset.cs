using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDrumset : MonoBehaviour
{
    public GameObject drummerPulse;   //projectile prefab
    public Transform firePoint;     //position from where to fire 
    public bool drumCanFire;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(drumCanFire);
        if (Input.GetKey(KeyCode.Space) && drumCanFire)
        {
            fire();
            drumCanFire = false;
        }
    }

    void fire()
    {
        Instantiate(drummerPulse, firePoint.position, firePoint.rotation);
    }
}
