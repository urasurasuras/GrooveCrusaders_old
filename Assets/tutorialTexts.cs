using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialTexts : MonoBehaviour
{

    public Text tutText;
    // Start is called before the first frame update
    void Start()
    {
        tutText.text= "Conductor, press Space on beat to activate the red button.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
