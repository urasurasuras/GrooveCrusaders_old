using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGuitar : MonoBehaviour
{
    public GameObject liteObject;   //projectile prefab
    public Transform firePoint;     //position from where to fire 
    public bool guitarCanFire;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(guitarCanFire);
        if (Input.GetKeyDown(KeyCode.Space) && guitarCanFire)
        {
            fire();
            guitarCanFire = false;
        }
    }

    void fire()
    {
        Instantiate(liteObject, firePoint.position, firePoint.rotation);
    }
}
