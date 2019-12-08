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
    public void backToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void reloadCurrentScene()
    {
        Scene current_scene = SceneManager.GetActiveScene();
        //SceneManager.UnloadScene(current_scene.name);
        SceneManager.LoadScene(current_scene.name);
    }
}
