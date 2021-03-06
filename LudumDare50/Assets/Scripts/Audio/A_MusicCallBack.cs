using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class A_MusicCallBack : MonoBehaviour
{
    public bool FMODIntroDoOnce = true;
    public bool CBDeath = false;
    public bool CBDoOnce = true;
    public bool AllowCeilingParam = false;
    string sExitPattern;
    class TimelineInfo
    {
        public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    public FMODUnity.EventReference musicCallBackInstance;
    public FMODUnity.EventReference menuCallBackInstance;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    public FMOD.Studio.EventInstance musicInstance;
    public FMOD.Studio.EventInstance menuInstance;

    void Reset()
    {
        //musicCallBackInstance = FMODUnity.EventReference.Find("event:/Music/Main_Music");
        //menuCallBackInstance = FMODUnity.EventReference.Find("event:/Music/Main_Menu");
    }

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        AudioManager.am.InitCB = true;
        menuInstance = FMODUnity.RuntimeManager.CreateInstance(menuCallBackInstance);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicCallBackInstance);

        MenuCB();
        MenuMusicStart();
        //InGameMusicStart();
    }

    public void MenuMusicStart()
    {
        if (menuInstance.isValid())
        {
            Debug.Log("Menu Music Started");
            menuInstance.start();
        }
    }
    public void MenuMusicStop()
    {
        menuInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //FMOD PARAMETER MASTER STOP
    }

    public void InGameMusicStart()
    {
        if (IsPlaying(musicInstance) == false)
        {
            Debug.Log("InGameMusicStart");
            musicInstance.start();
        }
    }
    public void InGameMusicStop()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void ResetMenuCB()
    {
        menuInstance.setUserData(IntPtr.Zero);
        timelineHandle.Free();
    }
    public void ResetMusicCB()
    {
        musicInstance.setUserData(IntPtr.Zero);
        timelineHandle.Free();
    }

    /*public void TempMenuReset()
    {
        menuInstance.setUserData(IntPtr.Zero);
        menuInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        menuInstance.release();
        timelineHandle.Free();
    }*/
    /*public void TempMusicReset()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }*/

    /*void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }*/

    public void MusicCB()
    {
        //Debug.Log("music CB");
        timelineInfo = new TimelineInfo();
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        timelineHandle = GCHandle.Alloc(timelineInfo);
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }
    public void MenuCB()
    {
        //Debug.Log("menu CB");        
        timelineInfo = new TimelineInfo();
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        timelineHandle = GCHandle.Alloc(timelineInfo);
        menuInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        menuInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
    }


    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    void OnGUI()
    {
        //GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.currentMusicBar, (string)timelineInfo.lastMarker));
    }
    
    void Update()
    {
        if ((string)timelineInfo.lastMarker == "CB_MenuToGame" && CBDoOnce == true) 
        {
            InGameMusicStart();
            FindObjectOfType<GameManager>().cutscene.GetComponent<Animator>().SetTrigger("play");
            CBDoOnce = false;
            A_Timer.a_timer.a_countTime = false;
            AudioManager.am.startTimerCB = false;
        }

        if ((string)timelineInfo.lastMarker == "ResetCB")
        {
            Debug.Log("Reset CB");
            CBDoOnce = true;
            MenuMusicStop();
            ResetMenuCB();
            MusicCB();
            
        }
            
        if ((string)timelineInfo.lastMarker == "Exit_1" || (string)timelineInfo.lastMarker == "Exit_2" || (string)timelineInfo.lastMarker == "Exit_3" || (string)timelineInfo.lastMarker == "E_FullStop")
        {
            
        }

        if ((string)timelineInfo.lastMarker == "Death")
        {
            CBDeath = true;
        }
        else
        {
            CBDeath = false;
        }

        if (timelineInfo.currentMusicBar >= 9 && (string)timelineInfo.lastMarker == "A")
        {
            if (FMODIntroDoOnce == false && GameState.gs.introFinished == true && AudioManager.am.FMODRestarted == true)
            {
                FMODIntroDoOnce = true;
                AllowCeilingParam = true;
                GameState.gs.introFinished = true;
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IntroSFXDuck", 0);
                AudioManager.am.FMOD_PlayCeilingLoop();
                Debug.Log("Skipped intro and play Ceiling Loop");

                //return;
            }

            else if (FMODIntroDoOnce == false && GameState.gs.introFinished == false)
            {
                AudioManager.am.FMODRestarted = false;
                GameState.gs.introFinished = true;
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IntroSFXDuck", 0);
                FMODIntroDoOnce = true;
                AllowCeilingParam = true;

                AudioManager.am.FMOD_PlayCeilingLoop();
                Debug.Log("Play Intro Sequence and Ceiling Loop after");
                //Debug.Log("SET SOMETHING FANCY PLAYING WHEN CEILING IS ON THRESHOLD");
            }
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

    /*void OriginalStartInstructions()
    {
        // ------------ timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        // ------------ beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        // ------------- musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicCallBackInstance);

        // Pin the class that will store the data modified during the callback
        // ------------- timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        // -------------- musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        // -------------- musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        // -------------- musicInstance.start();
    }*/
}