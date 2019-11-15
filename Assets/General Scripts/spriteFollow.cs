using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFollow : MonoBehaviour
{
    public GameObject objectOfSprite;

    public PlayerControl charScript;
    //public Transform positionTarget;    //used to follow gameObject
    //public Transform cameraAngle;       //used face the camera

    [SerializeField]float vertOffset;
    [SerializeField]float horizOffset;

    // Start is called before the first frame update
    void Start()
    {
        //cameraAngle = GameObject.Find("CameraMan").GetComponent<Transform>();

        //charScript = objectOfSprite.GetComponent<PlayerControl>();
        //positionTarget = objectOfSprite.GetComponent<Transform>();

        //posOfObj = objectOfSprite.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "mobile_healthbar")
        {
            if (/*cameraAngle && objectOfSprite &&*/ charScript.facingRight)
            {
                transform.eulerAngles = new Vector3(-30, 0, 0);

                //print(objectOfSprite + " is facing right: " + objectOfSprite.GetComponent<PlayerControl>().facingRight);
                //transform.rotation = new Quaternion(cameraAngle.rotation.x, objectOfSprite.rotation, 0, 0);
            }
            if (/*cameraAngle && objectOfSprite &&*/ !charScript.facingRight)
            {
                transform.eulerAngles = new Vector3(30, 180, 0);

                //print(objectOfSprite + " is facing right: " + objectOfSprite.GetComponent<PlayerControl>().facingRight);

                //transform.rotation = new Quaternion(30, 0, 0, 0);
            }
        }
        else if (gameObject.tag == "mobile_healthbar")
        {
            //transform.position = new Vector3(0, vertOffset, 0);
            transform.eulerAngles = new Vector3(-30, 0, 0);
        }
    }
}
