using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hotkeyMngr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("Tutorial");
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SceneManager.LoadScene("Boss1");
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene("Boss2");
        }

    }
}
