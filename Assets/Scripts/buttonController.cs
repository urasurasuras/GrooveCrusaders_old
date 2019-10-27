using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("J1a"))
        {
            theSR.sprite = pressedImage;
        }
        if (Input.GetButtonUp("J1a"))
        {
            theSR.sprite = defaultImage;
        }

    }

    
}
