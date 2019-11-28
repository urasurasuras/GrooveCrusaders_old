using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using EZCameraShake;
using UnityEngine.Events;

public class noteShooter : MonoBehaviour
{
    public static noteShooter instance;
    public bool playerCanAttack;
    //public float beatsPerMin;   //better if determined by FMOD

    public static UnityEvent markerOnEvent;
    public static UnityEvent barEvent;

    boosAnimationScript boss1Script;
    [StructLayout(LayoutKind.Sequential)]
    class TimelineInfo
    {
        public int currentMusicBar = 0;
        public int currentMusicBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    [FMODUnity.EventRef]
    public string LevelStateEvent = "";

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance;



    private float nextSpawnTime;
    Vector2 notePos;
    public GameObject arrow;
    [SerializeField]
    private float beatLength;
    public static string marker;
    public int latestBeat;
    public static int bar;
    public static int beat;
    public int latestBar;
    public static int bpm;

    void Start()
    {
        markerOnEvent = new UnityEvent();
        barEvent = new UnityEvent();
        markerOnEvent.AddListener(fireNote);
        instance = this;


        //FMOD shit
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(LevelStateEvent);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        //Debug.Log("CALLBACK");

        musicInstance.start();
        //
    }

    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }




    void OnGUI()
    {
        if (timelineInfo != null)
        {
            GUILayout.Box(String.Format("Current Bar = {0}.{1}", timelineInfo.currentMusicBar, timelineInfo.currentMusicBeat));
        }
    }




    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {

        //Debug.Log("callback");
        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
            if (parameter.tempo != 0)
                bpm = (int)parameter.tempo;
            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        //var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                        bar = timelineInfo.currentMusicBar;
                        //Debug.Log("bedug 1");

                        timelineInfo.currentMusicBeat = parameter.beat;
                        beat = timelineInfo.currentMusicBeat;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        //var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        //timelineInfo.lastMarker = parameter.name;
                        //marker = timelineInfo.lastMarker;

                        //var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        //timelineInfo.currentMusicBeat = parameter.beat;
                        //beat = timelineInfo.currentMusicBeat;

                        //markerEvent.Invoke();
                        //Debug.Log("bedug 2");

                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (!GameManager.Instance.game_paused)
            {
                musicInstance.setPaused(false);
            }
            else if (GameManager.Instance.game_paused)
            {
                musicInstance.setPaused(true);
            }
        }
        if (latestBeat != beat) {
            CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, .1f);
            latestBeat = beat;
            markerOnEvent.Invoke();
        }
        if (latestBar != bar)
        {
            //Debug.Log("Bar changed to: "+latestBar);
            latestBar = bar;
            barEvent.Invoke();
        }
        if (GameObject.Find("Boss1") != null)   //set boss at Start, then set FMOD param
        {
            boss1Script = GameObject.Find("Boss1").GetComponent<boosAnimationScript>();
            musicInstance.setParameterByName("Boss Health", boss1Script.boss1Health);
            //print(boss1Script.boss1Health);
        }
        musicInstance.setParameterByName("Player Health", (float)GameManager.Instance.minPlayerHP);
    }

    private bool shouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }

    void fireNote(){
        nextSpawnTime = Time.time + beatLength;
        Instantiate(arrow, transform.position, Quaternion.identity);
        //Debug.Log("fire");
    }

    public void noteHit()
    {
        //Debug.Log("HIT !");
    }
    public void noteMissed()
    {
        //Debug.Log("MISS !");
    }
}
