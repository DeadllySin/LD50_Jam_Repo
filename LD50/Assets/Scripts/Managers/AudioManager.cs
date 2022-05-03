using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    GameManager gm;

    public bool playIntroMusic = true;

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
    [HideInInspector] public FMOD.Studio.EventInstance menuMusicInstance;    


    public void Awake()
    {
        if (am != null)
        {
            Destroy(this);
            Debug.Log("AudioManager already exists or not loaded");
        }
        am = this;
        DontDestroyOnLoad(this.gameObject);

        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
    }
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        gameplayBus = FMODUnity.RuntimeManager.GetBus("bus:/Gameplay_Bus");
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music_Bus");
        UIBus = FMODUnity.RuntimeManager.GetBus("bus:/UI_Bus");
        
        menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main_Menu");
        menuMusicInstance.start();
    }
    void Update()
    {

    }
}
