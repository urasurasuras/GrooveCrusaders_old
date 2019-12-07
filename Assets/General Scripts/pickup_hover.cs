using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_hover : MonoBehaviour
{
    public AnimationCurve myCurve;

    void Update()
    {
        //transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }
}
