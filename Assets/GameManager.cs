﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<PlayerControl> playerList;
    public static GameManager Instance
    {
        get
        {
            return _instance;

        }
    }
    private static GameManager _instance;
    // Start is called before the first frame update
    void Start()
    {
        playerList = new List<PlayerControl>();
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void PlayerCanShoot(bool canShoot)
    {
        foreach(PlayerControl pc in playerList)
        {
            pc.playerCanFire = canShoot;
        }
    }

    public void RegisterPlayerControl(PlayerControl pc)
    {
        playerList.Add(pc);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
