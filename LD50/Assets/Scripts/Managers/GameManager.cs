using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private bool isDead;
    public GameObject player;
    public GameObject room;

    [HideInInspector] public GameObject currRoom;
    [HideInInspector] public string currRoomType;
    [HideInInspector] public Tunnel currTunnel;
    [HideInInspector] public int lastRoom = 0;
    [HideInInspector] public int roomsCleared = 0;
    [HideInInspector] public int statueRoomPro;

    FMOD.Studio.EventInstance ceilingLoopInstance;
    [HideInInspector] public FMOD.Studio.EventInstance ceilingDebrisInstance;
    [HideInInspector] public FMOD.Studio.EventInstance menuMusicInstance;
    FMOD.Studio.EventInstance mainMenuMusicInstance;

    [Header("UI")]
    [SerializeField] private Text scoreText;
    public Text lookingAtText;
    [SerializeField] private GameObject deathScreen;

    [Header("Ceiling")]
    public GameObject ceiling;
    [SerializeField] float slowThresholdSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float deathHeight;
    [SerializeField] private float thresholdToSlower;
    [HideInInspector] public float ceilingSpeed;
    [HideInInspector] public GameObject ceilingSourceChild;

    bool isPaused;

    private void Awake()
    {
        ceilingSpeed = defaultSpeed;
        gm = this;
        currRoom = FindObjectOfType<Room_Main>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
        ceilingSourceChild = player.transform.GetChild(3).gameObject;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        ceilingDebrisInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingDebris);
        ceilingDebrisInstance.start();
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
            }
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                Time.timeScale = 1;
            }

        }

        if (AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger == true)
        {
            //Debug.Log("ceilling moving in if -- musicIntroTrigger == true");
            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
        }
        else if (AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger == false && GameState.gs.playIntroMusic == false)
        {
            //Debug.Log("ceiling moving in else if -- musicIntroTrigger == false");
            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
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
        isDead = true;
        if (roomsCleared > PlayerPrefs.GetInt("roomsCleared")) PlayerPrefs.SetInt("roomsCleared", roomsCleared);
        scoreText.text = "You Cleared " + roomsCleared + " Rooms!\n Your Highscore is " + PlayerPrefs.GetInt("roomsCleared");
        deathScreen.SetActive(true);
        player.SetActive(false);
        
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.deathSFX);
        ceilingLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingLoopInstance.release();
        ceilingDebrisInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        ceilingDebrisInstance.release();

        // find a way to release in game music instance after death screen and restart 
    }

    public void FMOD_PlayCeilingLoop()
    {
        ceilingLoopInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.ceilingLoop);
        ceilingLoopInstance.start();
    }
}