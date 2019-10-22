using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boss2 : MonoBehaviour
{
    public float speed;

    public Transform targetPlayer;
    private Vector3 currentBossPos;
    private Vector3 lastPlayerPos;

    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        noteShooter.markerOnEvent.AddListener(setTarget);
        noteShooter.markerOnEvent.AddListener(chargeToPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("pressed F");
            setTarget();
        }
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
