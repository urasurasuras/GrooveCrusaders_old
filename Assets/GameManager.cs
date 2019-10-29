using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent onBeatEvent;
    public static UnityEvent redButtonCanPressEvent;
    List<PlayerControl> playerList;
    List<buttonController> buttonList;

    buttonController buttonControllerScript;


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
        onBeatEvent = new UnityEvent();
        redButtonCanPressEvent = new UnityEvent();
        playerList = new List<PlayerControl>();
        if (_instance == null)
        {
            _instance = this;
        }
        buttonControllerScript = GameObject.Find("Buttons_Red").GetComponent<buttonController>();
    }

    public void redButtonCanPress(bool canShoot)
    {
        buttonControllerScript.redButtonCanBePressed = canShoot;
        Debug.Log("red button can press: " + canShoot);
        if (buttonControllerScript.redButtonBeingPressed)
        {
            foreach (PlayerControl pc in playerList)
            {
                pc.playerCanFire = canShoot;

                Debug.Log("players can press: " + canShoot);

                //Invoke("setBoolBack(canShoot)", 0.5f);

            }
        }
        if (!buttonControllerScript.redButtonBeingPressed)
        {
            foreach (PlayerControl pc in playerList)
            {
                pc.playerCanFire = false;

                Debug.Log("players can press: " + canShoot);

                //Invoke("setBoolBack(canShoot)", 0.5f);

            }
        }
        
    }

    public void RegisterPlayerControl(PlayerControl pc)
    {
        playerList.Add(pc);
    }
   
    void FixedUpdate()
    {
        redButtonCanPressEvent.Invoke();
        onBeatEvent.Invoke();
    }
}
