using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFollow : MonoBehaviour
{
    //public GameObject objectOfSprite;
    public Transform positionTarget;    //used to follow gameObject
    public Transform angleTarget;       //used face the camera

    // Start is called before the first frame update
    void Start()
    {
        //posOfObj = objectOfSprite.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = positionTarget.position;
        transform.rotation = angleTarget.rotation;
    }
}
