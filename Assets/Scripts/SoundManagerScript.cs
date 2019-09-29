using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip pickupSound, winSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        //10. Made a “SoundManager” game object that hold an audio source component that plays music

        GameObject thePlayer = GameObject.Find("Player");
        PlayerControl PlayerControl = thePlayer.GetComponent<PlayerControl>();//Refers to item number 12
        PlayerControl.winCondition = false;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Refers to item number 12
        //if(winCondition == true)
        //{
        //    this.gameObject.SetActive(false);

        //}

    }


}
