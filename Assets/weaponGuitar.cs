using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGuitar : MonoBehaviour
{
    public GameObject liteObject;   //projectile prefab
    public Transform firePoint;     //position from where to fire 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) /*&& playerCanFire*/)
        {
            fire();
            //playerCanFire = false;
        }
    }

    void fire()
    {
        Instantiate(liteObject, firePoint.position, firePoint.rotation);
    }
}
