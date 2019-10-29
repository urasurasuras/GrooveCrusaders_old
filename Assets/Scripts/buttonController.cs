using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public bool noteCanBePressed;
    public bool redButtonCanBePressed;
    public bool redButtonBeingPressed;

    public static buttonController instance;
    noteScroll noteScript;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        redButtonBeingPressed = false;
        theSR = GetComponent<SpriteRenderer>();
        //GameManager.Instance.registerRedButton(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noteCanBePressed)
        {
            redButtonBeingPressed = true;
            Debug.Log("note can be pressed: "+noteCanBePressed);
            Invoke("setBoolBack",0.5f);
            theSR.sprite = pressedImage;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //redButtonBeingPressed = false;
            theSR.sprite = defaultImage;
        }
        GameManager.Instance.redButtonCanPress(redButtonBeingPressed);
        //if(!redButtonBeingPressed)
        //    GameManager.Instance.redButtonCanPress(false);

    }
    private void setBoolBack()
    {
        redButtonBeingPressed = false;
    }

    
}
