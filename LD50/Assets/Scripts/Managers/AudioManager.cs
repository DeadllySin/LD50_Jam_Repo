using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    GameManager gm;

    public bool FMODRestarted = false;

    [Header("Player")]
    public FMODUnity.EventReference footsteps; //movement
    public FMODUnity.EventReference deathSFX;

    [Header("Environment")]
    public FMODUnity.EventReference ceilingLoop;
    public FMODUnity.EventReference ceilingDebris;
    public FMODUnity.EventReference doorOpen;
    public FMODUnity.EventReference doorClose;

    [Header("Music")]
    public FMODUnity.EventReference inGameMusic;
    public FMODUnity.EventReference mainMenuMusic;
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
    public FMODUnity.EventReference pColorTimer;

    [Header("UI")]
    public FMODUnity.EventReference puzzleCorrect;
    public FMODUnity.EventReference puzzleWrong;
    public FMODUnity.EventReference uiClick;

    [Header("Bus")]
    public FMOD.Studio.Bus masterBus;
    public FMOD.Studio.Bus gameplayBus;
    public FMOD.Studio.Bus musicBus;
    public FMOD.Studio.Bus UIBus;

    [Header("Snapshots")]
    public FMODUnity.EventReference pauseSS;

    //Generic Enviromental and Audio Instances
    [HideInInspector] public FMOD.Studio.EventInstance ceilingDebrisInstance;
    [HideInInspector] public FMOD.Studio.EventInstance menuMusicInstance;
    [HideInInspector] public FMOD.Studio.EventInstance ceilingLoopInstance;
    
    //Snapshots
    [HideInInspector] public FMOD.Studio.EventInstance pauseSSInstance;

    //Slider
    [SerializeField] [Range (0f, 100f)]
    //public float masterVolume;

    public float masterVolume;

    public void Awake()
    {
        if (am != null)
        {
            Destroy(this);
            Debug.Log("AudioManager already exists or not loaded");
        }
        am = this;
        DontDestroyOnLoad(this.gameObject);

        //FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");
        //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
    }
    void Start()
    {
        FMODRestarted = false;

        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        gameplayBus = FMODUnity.RuntimeManager.GetBus("bus:/Gameplay_Bus");
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music_Bus");
        UIBus = FMODUnity.RuntimeManager.GetBus("bus:/UI_Bus");

        masterVolume = 100f;

        menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main_Menu");
        pauseSSInstance = FMODUnity.RuntimeManager.CreateInstance(this.pauseSS);
        if (GameState.gs.skipCutscene == true)
        {
            FMOD_InGameState();
        }
        else
        {
            FMOD_MainMenuState();
        }
    }

    public void FMOD_InGameState()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
        
        am.menuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        am.menuMusicInstance.release();
        am.GetComponent<A_MusicCallBack>().musicInstance.start();
        am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = false;
        FMODRestarted = true;
    }

    public void FMOD_MainMenuState()
    {
        FMODRestarted = false;

        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
        pauseSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMOD_StopCeilingLoop();
        menuMusicInstance.start();
    }

    public void FMOD_PauseState()
    {
        pauseSSInstance.start();
    }

    public void FMOD_DeadState()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(am.deathSFX);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);

        ceilingLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingLoopInstance.release();
        ceilingDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingDebrisInstance.release();
    }

    public void FMOD_PlayCeilingLoop()
    {
        ceilingDebrisInstance = FMODUnity.RuntimeManager.CreateInstance(am.ceilingDebris);
        ceilingDebrisInstance.start();
        ceilingLoopInstance = FMODUnity.RuntimeManager.CreateInstance(am.ceilingLoop);
        ceilingLoopInstance.start();
    }

    public void FMOD_StopCeilingLoop()
    {
        ceilingDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingDebrisInstance.release();
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

        FMOD_MasterSlider();


    }

    public void FMOD_MasterSlider()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("OptionsVolume", masterVolume);
    }
}
