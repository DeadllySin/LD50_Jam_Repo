using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    bool dieOnlyOnce;
    public GameObject currRoom;
    public Tunnel currTunnel;
    public GameObject player;
    public GameObject ceiling;
    public GameObject[] roomList;
    public string whatRoom;
    [HideInInspector] public int lastRoom = 0;
    [SerializeField] private float ceilingSpeed;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private float deathHeight;
    [SerializeField] private float fastHeight;

    FMOD.Studio.EventInstance ceilingLoopInstance;
    FMOD.Studio.EventInstance ceilingDebrisInstance;
    FMOD.Studio.EventInstance mainMenuMusicInstance;
    FMOD.Studio.EventInstance inGameMusicInstance;

    private void Awake()
    {
        gm = this;
    }

    private void Start()
    {
        ceilingLoopInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingLoop);
        ceilingLoopInstance.start();
        ceilingDebrisInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingDebris);
        ceilingDebrisInstance.start();
        //mainMenuMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.mainMenuMusic);
        //mainMenuMusicInstance.start(); ---- Depends on the main menu
        //inGameMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.inGameMusic);
        //inGameMusicInstance.start(); ---- Depends on the main menu
    }

    private void Update()
    {
        if (ceiling.transform.position.y <= fastHeight && !dieOnlyOnce)
        {
            Debug.Log("You Are Dead");
            player.SetActive(false);
            ceilingSpeed *= 2;
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
            }
        }

        ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceiling));
        ceilingDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceiling));
        
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", ceiling.transform.position.y);
        //Debug.Log("Height: " + ceiling.transform.position.y);
        
        ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
    }
}