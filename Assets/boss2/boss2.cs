using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class boss2 : MonoBehaviour
{
    public float speed;

    public Transform targetPlayer;
    private Vector3 currentBossPos;
    private Vector3 lastPlayerPos;
    public Slider healtBar;
    public int bossHealth = 100;

    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        noteShooter.barEvent.AddListener(setTarget);
        //noteShooter.barEvent.AddListener(chargeToPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        healtBar.value = bossHealth;
        /*if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("pressed F");
            setTarget();
        }*/
        if (moving)
        {
            chargeToPlayer();
        }
    }

    public void setTarget()
    {
        lastPlayerPos = targetPlayer.position;
        moving = true;
    }
    public void chargeToPlayer()
    {

        Debug.Log("in charge func");

        Debug.Log(lastPlayerPos);
        Debug.Log(this.transform);

        if (transform.position != lastPlayerPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPlayerPos, speed * Time.deltaTime);
        }
    }


}
