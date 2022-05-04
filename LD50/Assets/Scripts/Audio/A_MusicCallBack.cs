using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class A_MusicCallBack : MonoBehaviour
{
    public bool musicIntroTrigger = true;

    class TimelineInfo
    {
        public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public FMODUnity.EventReference musicCallBackInstance;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    public FMOD.Studio.EventInstance musicInstance;

    void Reset()
    {
        musicCallBackInstance = FMODUnity.EventReference.Find("event:/Music/Main_Music");
    }

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicCallBackInstance);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        //musicInstance.start();

        // control variables
        //musicIntroTrigger = false;
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
        GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.currentMusicBar, (string)timelineInfo.lastMarker));
    }

    void Update()
    {
        if (timelineInfo.currentMusicBar >= 9 && musicIntroTrigger == false && AudioManager.am.playIntroMusic == false)
        {
            
            GameManager.gm.GetComponent<GameManager>().FMOD_PlayCeilingLoop();
            Debug.Log("Playing skipped intro Ceiling Loop");
            musicIntroTrigger = true;

            return;
        }
        else if (timelineInfo.currentMusicBar >= 9 && AudioManager.am.playIntroMusic == true)
        {
            musicIntroTrigger = true;
            AudioManager.am.playIntroMusic = false;
            
            GameManager.gm.GetComponent<GameManager>().FMOD_PlayCeilingLoop();
            Debug.Log("Play Intro Music and Ceiling Loop after");
            //Debug.Log("SET SOMETHING FANCY PLAYING WHEN CEILING IS ON THRESHOLD");

        }

    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

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
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}