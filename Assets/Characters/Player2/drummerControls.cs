using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.UI;
using UnityEngine.Events;

public class drummerControls : MonoBehaviour
{
    public static UnityEvent playerTouchingEnemy;
    public double playerHealth = 100;
    public bool gameStarted;
    public float speed;
    private Rigidbody2D rb2d;
    private bool facingRight;
    public bool playerCanFire;

    // Start is called before the first frame update
    void Start()
    {
        playerTouchingEnemy = new UnityEvent();
        //GameManager.Instance.RegisterPlayerControl(this);
        gameStarted = false;
        rb2d = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
