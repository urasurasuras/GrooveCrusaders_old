using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadSceneButton : MonoBehaviour
{
    public string level;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void loadscene()
    {
        SceneManager.LoadScene(level);
    }
}
