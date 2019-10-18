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
    public bool gameStarted = false;
    //public float beatsPerMin;   //better if determined by FMOD

    public static UnityEvent markerEvent;
    public static UnityEvent barEvent;

    [StructLayout(LayoutKind.Sequential)]
    class TimelineInfo
    {
        public int currentMusicBar = 0;
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
    public string latestMarker;
    public static int bar;
    public int latestBar;

    void Start()
    {
        markerEvent = new UnityEvent();
        barEvent = new UnityEvent();
        markerEvent.AddListener(fireNote);
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

        //beatLength = 60f / beatsPerMin;
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
        GUILayout.Box(String.Format("Current Bar = {0}{1}", timelineInfo.currentMusicBar, (string)timelineInfo.lastMarker));
        GUILayout.Box(marker);
        //Debug.Log("GUI");
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

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                        bar = timelineInfo.currentMusicBar;
                        //Debug.Log("bedug 1");
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        marker = timelineInfo.lastMarker;
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
        if (latestMarker != marker) {
            CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, .1f);
            latestMarker = marker;
            markerEvent.Invoke();
        }
        if (latestBar != bar)
        {
            latestBar = bar;
            barEvent.Invoke();
        }
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
