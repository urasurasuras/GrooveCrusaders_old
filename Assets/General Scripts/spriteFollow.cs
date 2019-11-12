using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFollow : MonoBehaviour
{
    //public GameObject objectOfSprite;
    public Transform positionTarget;    //used to follow gameObject
    public Transform angleTarget;       //used face the camera
    [SerializeField]float vertOffset;
    [SerializeField]float horizOffset;

    // Start is called before the first frame update
    void Start()
    {
        angleTarget = GameObject.Find("CameraMan").GetComponent<Transform>();
        //posOfObj = objectOfSprite.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //vertOffset = positionTarget.position.y;
        //vertOffset += 1;

        //transform.position = positionTarget.position;
        transform.position = new Vector3(positionTarget.position.x + horizOffset, positionTarget.position.y + vertOffset, positionTarget.position.z);
        transform.rotation = angleTarget.rotation;
        //transform.rotation = positionTarget.rotation;
    }
}
