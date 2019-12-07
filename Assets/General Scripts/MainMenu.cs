using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool play_tut = true;
    public void ask_tut(bool toggle)   //Change bool based on toggle checkbox
    {
        play_tut = toggle;
    }
    public void playGame()
    {
        if (play_tut)
        {
            SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(2);
    }
}
