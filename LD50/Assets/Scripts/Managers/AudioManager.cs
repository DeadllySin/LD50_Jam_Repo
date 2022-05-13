using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;

    [Header("Player")]
    public FMODUnity.EventReference footsteps;
    public FMODUnity.EventReference deathSFX;
    public FMODUnity.EventReference introSteps;

    [Header("Environment")]
    public FMODUnity.EventReference ceilingLoop;
    public FMODUnity.EventReference ceilingFeedbackDebris;
    public FMODUnity.EventReference doorOpen;
    public FMODUnity.EventReference doorClose;

    [Header("Music")]
    public FMODUnity.EventReference inGameMusic; //controlled with Call Back
    public FMODUnity.EventReference mainMenuMusic; //controlled with Call Back
    public FMODUnity.EventReference deadMusic;
    public FMODUnity.EventReference introStinger;

    [Header("Puzzle Combine")]
    public FMODUnity.EventReference pInsertPiece;
    public FMODUnity.EventReference pRemovePiece;
    public FMODUnity.EventReference pPickUp;
    public FMODUnity.EventReference pDrop;

    [Header("Puzzle Math")]
    public FMODUnity.EventReference pSlideUp;
    public FMODUnity.EventReference pSlideDown;
    public FMODUnity.EventReference pConfirm;

    [Header("Puzzle Color")]
    public FMODUnity.EventReference pColorPress;
    public FMODUnity.EventReference pColorStart;
    public FMODUnity.EventReference pColorOrderGreen;
    public FMODUnity.EventReference pColorOrderBlue;
    public FMODUnity.EventReference pColorOrderYellow;
    public FMODUnity.EventReference pColorOrderOrange;
    public FMODUnity.EventReference pColorOrderRed;   

    [Header("UI")]
    public FMODUnity.EventReference puzzleCorrect;
    public FMODUnity.EventReference puzzleWrong;
    public FMODUnity.EventReference puzzleFullWrong;
    public FMODUnity.EventReference uiClick;
    public FMODUnity.EventReference uiSelect;

    [Header("Bus")]
    public FMOD.Studio.Bus masterBus;
    public FMOD.Studio.Bus gameplayBus;
    public FMOD.Studio.Bus musicBus;
    public FMOD.Studio.Bus UIBus;

    [Header("Snapshots")]
    public FMODUnity.EventReference reverbTunnelSS;
    public FMODUnity.EventReference pauseSS;
    public FMODUnity.EventReference ceilingFeedbackSS;
    public FMODUnity.EventReference tunnelOcclusionSS;

    //Generic Enviromental and Audio Instances
    [HideInInspector] public FMOD.Studio.EventInstance ceilingFBDebrisInstance;
    [HideInInspector] public FMOD.Studio.EventInstance ceilingLoopInstance;

    //Snapshots
    [HideInInspector] public FMOD.Studio.EventInstance reverbTunnelSSInstance;
    [HideInInspector] public FMOD.Studio.EventInstance pauseSSInstance;
    [HideInInspector] public FMOD.Studio.EventInstance ceilingFeedbackSSInstance;
    [HideInInspector] public FMOD.Studio.EventInstance tunnelOcclusionSSInstance;

    public bool FMODRestarted = false;
    bool AMDoOnce = true;

    public float initialTimeCB = 0f;
    public float finalTimeCB;
    public bool startTimerCB = false;
    public bool MenuCB = false;
    public bool InitCB = false;
    public void Awake()
    {
        if (am != null)
        {
            Destroy(this);
            Debug.Log("AudioManager already exists or not loaded");
        }
        am = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        gameplayBus = FMODUnity.RuntimeManager.GetBus("bus:/Gameplay_Bus");
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music_Bus");
        UIBus = FMODUnity.RuntimeManager.GetBus("bus:/UI_Bus");

        gameplayBus.setMute(true);

        pauseSSInstance = FMODUnity.RuntimeManager.CreateInstance(this.pauseSS);
        ceilingFeedbackSSInstance = FMODUnity.RuntimeManager.CreateInstance(this.ceilingFeedbackSS);
        reverbTunnelSSInstance = FMODUnity.RuntimeManager.CreateInstance(reverbTunnelSS);
        tunnelOcclusionSSInstance = FMODUnity.RuntimeManager.CreateInstance(tunnelOcclusionSS);

        if (GameState.gs.skipCutscene == true)
        {
            FMOD_InGameState();
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
        }
        else
        {
            FMOD_MainMenuState();
        }
    }

    public void FMOD_MenuCBTimer()
    {
        //timer
        float t = Time.time;
        finalTimeCB = (t % 60);
        //Debug.Log(finalTimeCB); starts counting at start
    }

    public void FixedUpdate()
    {
        if (startTimerCB == true)
        {
            FMOD_MenuCBTimer();
        }

        if (AMDoOnce == true && MenuCB == true)
        {
            AMDoOnce = false;
            MenuCB = false;
        }
        
    }

    public void FMOD_CeilingFasterOneShot()
    {
        Debug.Log("ok puzzle call feedback");
        StartCoroutine(CeilingFeedbackDebris());
    }

    IEnumerator CeilingFeedbackDebris()
    {
        ceilingFeedbackSSInstance.start();
        yield return new WaitForSeconds(1f);
        ceilingFBDebrisInstance.start();
        yield return new WaitForSeconds(1f);
        ceilingFeedbackSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void FMOD_InGameState()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
        
        am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = false;

        //Menu music Stop in timeline ResetCB
        //In Game Music started in timeline CB_MenuToGame

        gameplayBus.setMute(false);
    }

    public void FMOD_MainMenuState()
    {
        FMODRestarted = false;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
        pauseSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD_StopCeilingLoop();
        am.GetComponent<A_MusicCallBack>().InGameMusicStop();
        //don't reset before starting
        //am.GetComponent<A_MusicCallBack>().InGameMusicStop();
        //am.GetComponent<A_MusicCallBack>().musicInstance.release();
        //Menu drone or music riser is louder the second time but isntances are not duplicated???
        if (InitCB == false)
        {
            Debug.Log("init false");
            am.GetComponent<A_MusicCallBack>().ResetMusicCB();
            am.GetComponent<A_MusicCallBack>().InGameMusicStop();
            
            am.GetComponent<A_MusicCallBack>().MenuCB();
            am.GetComponent<A_MusicCallBack>().MenuMusicStart(); //starts in fmod main music command event
        }
        InitCB = false;
        gameplayBus.setMute(true);
    }

    public void FMOD_PauseState()
    {
        pauseSSInstance.start();
    }

    public void FMOD_DeadState()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);

        ceilingLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingLoopInstance.release();
        ceilingFBDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingFBDebrisInstance.release();
    }

    public void FMOD_PlayCeilingLoop()
    {
        ceilingFBDebrisInstance = FMODUnity.RuntimeManager.CreateInstance(am.ceilingFeedbackDebris);
        ceilingFBDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(GameManager.gm.ceilingSourceChild));
        ceilingLoopInstance = FMODUnity.RuntimeManager.CreateInstance(am.ceilingLoop);
        ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(GameManager.gm.ceilingSourceChild));
        ceilingLoopInstance.start();
    }

    public void FMOD_StopCeilingLoop()
    {
        ceilingFBDebrisInstance.release();
        ceilingLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingLoopInstance.release();
    }

    public void FMOD_LoadInGameInstances()
    {

    }
    void Update()
    {
        if (GameState.gs.introFinished == true)
        {
            if (FMODRestarted == true)
            {
                //ceilingDebrisInstance.start();
            }
        }
        else if (GameState.gs.introFinished == false)
        {
            //ceilingDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
