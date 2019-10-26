using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject liteObject;   //projectile prefab
    public Transform firePoint;     //position from where to fire 
    public bool canFire;
    // Start is called before the first frame update
    protected void Start()
    {
       
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            fire();
            canFire = false;
        }
    }
    protected void fire()
    {
        Instantiate(liteObject, firePoint.position, firePoint.rotation);
    }
}
