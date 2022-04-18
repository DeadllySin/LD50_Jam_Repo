using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    bool dieOnlyOnce;
    [SerializeField] private GameObject blackScreen;
    public GameObject player;
    public GameObject ceiling;
    public GameObject room;
    [SerializeField] private float ceilingSpeed;
    [SerializeField] private float deathHeight;
    [SerializeField] private float thresholdToSlower;
    [HideInInspector] public GameObject currRoom;
    [HideInInspector] public string currRoomType;
    [HideInInspector] public Tunnel currTunnel;
    [HideInInspector] public int lastRoom = 0;
    [SerializeField] float slowThresholdSpeed = 1.5f;

    FMOD.Studio.EventInstance ceilingLoopInstance;
    FMOD.Studio.EventInstance ceilingDebrisInstance;
    FMOD.Studio.EventInstance mainMenuMusicInstance;
    FMOD.Studio.EventInstance inGameMusicInstance;
    public GameObject ceilingSourceChild;

    private void Awake()
    {
        gm = this;
        currRoom = FindObjectOfType<Main_Room>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
    }

    /*bool isMusicPlaying(FMOD.Studio.EventInstance inGameMusicInstance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        inGameMusicInstance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }*/
    private void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game"); //change to menu once we have 
        ceilingLoopInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingLoop);
        ceilingLoopInstance.start();
        ceilingDebrisInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingDebris);
        ceilingDebrisInstance.start();
        //mainMenuMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.mainMenuMusic);
        //mainMenuMusicInstance.start(); ---- Depends on the main menu
        inGameMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.inGameMusic);
        inGameMusicInstance.start(); //---- Depends on the main menu

        ceilingSourceChild = player.transform.GetChild(3).gameObject;
    }

    private void Update()
    {
        if (ceiling.transform.position.y <= thresholdToSlower && !dieOnlyOnce)
        {
            player.SetActive(false);
            ceilingSpeed = slowThresholdSpeed;
            if (ceiling.transform.position.y < deathHeight)
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.deathSFX);
                ceilingLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                ceilingLoopInstance.release();
                ceilingDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                ceilingDebrisInstance.release();
                blackScreen.SetActive(true); 
                dieOnlyOnce = true;

                // find a way to release in game music instance after death screen and restart 
                
                /*if (isMusicPlaying(inGameMusicInstance))
                {
                    inGameMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    inGameMusicInstance.release();
                }*/
            }
        }
        ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);

        
        //Fmod stuff
        ceilingSourceChild.transform.position = new Vector3(player.transform.position.x, ceiling.transform.position.y - 0.5f, player.transform.position.z);
        ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
        ceilingDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", ceilingSourceChild.transform.position.y);
        Debug.Log("Height: " + ceilingSourceChild.transform.position.y);
    }
}