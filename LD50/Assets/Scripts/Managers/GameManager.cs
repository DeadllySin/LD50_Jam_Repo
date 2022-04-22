using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private bool isDead;
    public GameObject player;
    public GameObject ceiling;
    public GameObject room;
    [SerializeField] private Text scoreText;
    [SerializeField] float slowThresholdSpeed = 1.5f;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private float ceilingSpeed;
    [SerializeField] private float deathHeight;
    [SerializeField] private float thresholdToSlower;
    [HideInInspector] public GameObject currRoom;
    [HideInInspector] public string currRoomType;
    [HideInInspector] public Tunnel currTunnel;
    [HideInInspector] public int lastRoom = 0;
    [HideInInspector] public int roomsCleared = 0;

    FMOD.Studio.EventInstance ceilingLoopInstance;
    FMOD.Studio.EventInstance ceilingDebrisInstance;
    FMOD.Studio.EventInstance mainMenuMusicInstance;
    public GameObject ceilingSourceChild;

    private void Awake()
    {
        gm = this;
        currRoom = FindObjectOfType<Room_Main>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
        ceilingSourceChild = player.transform.GetChild(3).gameObject;
    }

    private void Start()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game"); //change to menu once we have 
        ceilingDebrisInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingDebris);
        ceilingDebrisInstance.start();
        //mainMenuMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.mainMenuMusic);
        //mainMenuMusicInstance.start(); ---- Depends on the main menu

    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (ceiling.transform.position.y <= thresholdToSlower)
            {
                ceilingSpeed = slowThresholdSpeed;
                if (ceiling.transform.position.y < deathHeight) OnDeath();
            }
        }

    }


    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }

        if (AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger == true)
        {
            //Debug.Log("ceilling moving in if");
            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
        }
        else if (AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger == false && GameState.gs.playIntroMusic == false)
        {
            //Debug.Log("ceiling moving in else if"); TEST AFTER RESTART 
            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y + 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
        }

        //Fmod stuff
        ceilingSourceChild.transform.position = new Vector3(player.transform.position.x, ceiling.transform.position.y - 0.5f, player.transform.position.z);
        ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
        ceilingDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", ceilingSourceChild.transform.position.y);
        //Debug.Log("Height: " + ceilingSourceChild.transform.position.y);
    }

    private void OnDeath()
    {
        if(roomsCleared > PlayerPrefs.GetInt("roomsCleared")) PlayerPrefs.SetInt("roomsCleared", roomsCleared);
        isDead = true;
        scoreText.text = "You Cleared " + roomsCleared + " Rooms!\n Your Highscore is " + PlayerPrefs.GetInt("roomsCleared");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.deathSFX);
        ceilingLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingLoopInstance.release();
        ceilingDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingDebrisInstance.release();
        blackScreen.SetActive(true);
        player.SetActive(false);
        // find a way to release in game music instance after death screen and restart 
    }

    public void FMOD_PlayCeilingLoops()
    {
        ceilingLoopInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingLoop);
        ceilingLoopInstance.start();
    }
}