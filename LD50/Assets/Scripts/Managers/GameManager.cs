using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Main")]
    public static GameManager gm;
    private bool isDead;
    public GameObject player;
    public GameObject room;
    public Light l;
    [HideInInspector] public GameObject currRoom;
    [HideInInspector] public string currRoomType;
    [HideInInspector] public Tunnel currTunnel;
    [HideInInspector] public int lastRoom = 0;
    [HideInInspector] public int roomsCleared = 0;
    [HideInInspector] public int statueRoomPro;
    [HideInInspector] public int colorRoomPro;
    [HideInInspector] public int ringRoomPro;
    [SerializeField] private GameObject playerCine;
    [SerializeField] private GameObject dustStorm;

    [Header("Cutscene")]
    [SerializeField] public GameObject cutscene;
    [SerializeField] private GameObject cutsceneCam;
    [SerializeField] private GameObject door;

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseScreen;
    public GameObject mainMenu;

    [Header("Ceiling")]
    public GameObject ceiling;
    public float speedBoost;
    [SerializeField] float slowThresholdSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float deathHeight;
    [SerializeField] private float thresholdToSlower;
    [HideInInspector] public float ceilingSpeed;
    [HideInInspector] public GameObject ceilingSourceChild;
    private bool doOnce;
    private float startTime;
    private string minutes;
    private string seconds;


    private void Awake()
    {
        gm = this;
        dustStorm.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        playerCine.SetActive(GameState.gs.skipCutscene);
        cutsceneCam.SetActive(!GameState.gs.skipCutscene);
        cutscene.SetActive(!GameState.gs.skipCutscene);
        door.SetActive(GameState.gs.skipCutscene);
        player.SetActive(GameState.gs.skipCutscene);
        //defaultSpeed = ceilingSpeedScale(defaultSpeed, 0f, 10f, 0f, 0.5f);
        mainMenu.SetActive(!GameState.gs.skipCutscene);
        startTime = Time.time;
        ceilingSpeed = defaultSpeed;
        currRoom = FindObjectOfType<Room_Main>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
        ceilingSourceChild = player.transform.GetChild(3).gameObject;
    }

    private void FixedUpdate()
    {
        if (player.transform.position.y < -1) OnDeath();
        if (!isDead)
        {
            if (ceiling.transform.position.y <= thresholdToSlower)
            {
                ceilingSpeed = slowThresholdSpeed;
                if (ceiling.transform.position.y < deathHeight)
                {
                    FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Dead");
                    if (AudioManager.am.GetComponent<A_MusicCallBack>().CBDeath == true) OnDeath();
                }
            }
            if (GameState.gs.introFinished)
            {
                if (!doOnce)
                {
                    doOnce = true;
                    startTime = Time.time;
                    dustStorm.SetActive(false);
                    l.gameObject.SetActive(false);
                    Timer.timer.countTime = true;
                }
                Debug.Log("ceiling is moving");


                //Fmod stuff
                ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
                ceilingSourceChild.transform.position = new Vector3(player.transform.position.x, ceiling.transform.position.y - 0.5f, player.transform.position.z);
                AudioManager.am.ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
                AudioManager.am.ceilingFBDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
                if (AudioManager.am.GetComponent<A_MusicCallBack>().AllowCeilingParam == true)
                {
                    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", ceilingSourceChild.transform.position.y);
                }
                else
                {
                    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", 11f);
                }
            }
        }
    }

    /*public static float ceilingSpeedScale(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && GameState.gs.introFinished) Pause();
    }

    private void OnDeath()
    {
        //Debug.Log("OnDeath Game Manager");
        Destroy(FindObjectOfType<Room_Main>().gameObject);
        isDead = true;
        Timer.timer.countTime = false;
        if (roomsCleared > PlayerPrefs.GetInt("roomsCleared")) PlayerPrefs.SetInt("roomsCleared", roomsCleared);
        scoreText.text = "you cleared " + roomsCleared + " rooms!\n your highscore is " + PlayerPrefs.GetInt("roomsCleared") + "\nyou survived for " + Timer.timer.time + "\npress r to restart\npress m return to main menu\npress esc to quit";
        deathScreen.SetActive(true);
        player.SetActive(false);
        AudioManager.am.FMOD_DeadState();
        AudioManager.am.GetComponent<A_MusicCallBack>().AllowCeilingParam = false;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", 11f);

        //timer
        float t = Time.time - startTime;
        minutes = ((int)t / 60).ToString();
        seconds = (t % 60).ToString("f0");
        Debug.Log(minutes + " mins" + seconds + " secs");
    }

    public void Pause()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        player.SetActive(!player.activeSelf);
        //if(!player.activeInHierarchy) AudioManager.am.pauseSSInstance.start();
        //else AudioManager.am.pauseSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}