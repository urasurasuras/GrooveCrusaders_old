using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using EZCameraShake;

public class noteShooter : MonoBehaviour
{
    public bool gameStarted = false;
    public float beatsPerMin;   //better if determined by FMOD

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

    void Start()
    {
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

        beatLength = 60f / beatsPerMin;
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
                        //Debug.Log("bedug 1");
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        marker = timelineInfo.lastMarker;
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
        //if (!gameStarted)
        //{
        //    if (Input.anyKeyDown) { gameStarted = true; }
        //}
        //if (shouldSpawn())
        //{
            if (latestMarker != marker) {
                fireNote();
            CameraShaker.Instance.ShakeOnce(1f, 1f, .1f, .1f);
                latestMarker = marker;
            }
        //}
    }

    private bool shouldSpawn()
    {
        return Time.time >= nextSpawnTime;
    }

    void fireNote(){
        nextSpawnTime = Time.time + beatLength;
        Instantiate(arrow, transform.position, Quaternion.identity);
        Debug.Log("fire");
    }
}
