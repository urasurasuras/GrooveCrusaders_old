using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGui : MonoBehaviour
{
    public Text cont0, cont1, cont2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            cont0.text = Input.GetJoystickNames()[0];
        }catch (System.IndexOutOfRangeException e)
        {
            cont0.text = "No controller connected, move by WASD";
        }
        try
        {
            cont1.text = Input.GetJoystickNames()[1];
        }catch (System.IndexOutOfRangeException e)
        {
            cont1.text = "No controller connected, move by WASD";
        }
        try
        {
            cont2.text = Input.GetJoystickNames()[2];
        }catch (System.IndexOutOfRangeException e)
        {
            cont2.text = "No controller connected, move by WASD";
        }        
    }
}
