using UnityEngine;
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
    [HideInInspector] public int colorRoomPro;
    [HideInInspector] public int ringRoomPro;
    [SerializeField] private GameObject playerCine;
    [SerializeField] private GameObject cutsceneCine;
    [SerializeField] private GameObject door;
    
    [Header("UI")]
    [SerializeField] private Text scoreText;
    public Text lookingAtText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Ceiling")]
    public GameObject ceiling;
    public float speedBoost;
    [SerializeField] float slowThresholdSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float deathHeight;
    [SerializeField] private float thresholdToSlower;
    [HideInInspector] public float ceilingSpeed;
    [HideInInspector] public GameObject ceilingSourceChild;

    private float startTime;
    private string minutes;
    private string seconds;


    private void Awake()
    {
        gm = this;
        //defaultSpeed = ceilingSpeedScale(defaultSpeed, 0f, 10f, 0f, 0.5f);
        //Debug.Log(defaultSpeed);
        //Speed up testing
        //Time.timeScale = 2;
        //Timer
        startTime = Time.time;
        ceilingSpeed = defaultSpeed;
        currRoom = FindObjectOfType<Room_Main>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
        ceilingSourceChild = player.transform.GetChild(3).gameObject;
    }

    public void Start()
    {
        if (!GameState.gs.skipCutscene)
        {
            Debug.Log("not skipped");
            cutsceneCine.SetActive(true);
        }
        else
        {
            Debug.Log("skipped");
            playerCine.SetActive(true);
            door.SetActive(true);
            
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        }
    }
    private void FixedUpdate()
    {
        Debug.Log(AudioManager.am.GetComponent<A_MusicCallBack>().CBDeath);
        if (!isDead)
        {
            if (ceiling.transform.position.y <= thresholdToSlower)
            {
                ceilingSpeed = slowThresholdSpeed;
                if (ceiling.transform.position.y < deathHeight)
                {
                    FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
                    if (AudioManager.am.GetComponent<A_MusicCallBack>().CBDeath == true)
                    {
                        OnDeath();
                    }
                }
            }
        }
    }

    public static float ceilingSpeedScale(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
    private void Update()
    {
        if (GameState.gs.introFinished == true) //|| AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce == true)
        {
            Debug.Log("ceiling is moving");
            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);

        }

        /*else if (AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger == false && AudioManager.am.playIntroMusic == false)
        {
            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
        }*/

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pauseScreen.activeSelf)//unpause
            {
                pauseScreen.SetActive(false);
                player.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("unpause");
                
                AudioManager.am.pauseSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            else if (!pauseScreen.activeSelf && !mainMenu.activeSelf && GameState.gs.introFinished == true) //pause
            {
                pauseScreen.SetActive(true);
                player.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("pause");
                
                AudioManager.am.pauseSSInstance.start();
            }

        }

        //Fmod stuff
        ceilingSourceChild.transform.position = new Vector3(player.transform.position.x, ceiling.transform.position.y - 0.5f, player.transform.position.z);
        AudioManager.am.ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
        AudioManager.am.ceilingDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
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
        AudioManager.am.FMOD_DeadState();

        //timer
        float t = Time.time - startTime;
        minutes = ((int)t / 60).ToString();
        seconds = (t % 60).ToString("f0");
        Debug.Log(minutes + " mins" + seconds + " secs");
    }
}