using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBass : MonoBehaviour
{
    public GameObject liteObject;   //projectile prefab
    public Transform firePoint;     //position from where to fire 
    public bool bassCanFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) /*&& bassCanFire*/)
        {
            fire();
            bassCanFire = false;
        }
    }
    void fire()
    {
        Instantiate(liteObject, firePoint.position, firePoint.rotation);
    }
}
