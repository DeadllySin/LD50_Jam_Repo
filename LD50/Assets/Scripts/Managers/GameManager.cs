using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Main")]
    public static GameManager gm;
    private bool isDead;
    public GameObject player;
    public GameObject room;
    [SerializeField] private Light l;
    private bool doOnce;
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
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject cutsceneCam;
    [SerializeField] private GameObject door;

    [Header("UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Ceiling")]
    public GameObject ceiling;
    public float speedBoost;
    private bool testOutDeath;
    [SerializeField] float slowThresholdSpeed;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float deathHeight;
    [SerializeField] private float thresholdToSlower;
    [HideInInspector] public float ceilingSpeed;
    [HideInInspector] public GameObject ceilingSourceChild;

    private void Awake()
    {
        gm = this;
        GameState.gs.introFinished = GameState.gs.skipCutscene;
        testOutDeath = GameState.gs.killFast;
        Cursor.lockState = CursorLockMode.Locked;
        mainMenu.SetActive(!GameState.gs.introFinished);
        playerCine.SetActive(GameState.gs.skipCutscene);
        cutsceneCam.SetActive(!GameState.gs.skipCutscene);
        cutscene.SetActive(!GameState.gs.skipCutscene);
        door.SetActive(GameState.gs.skipCutscene);
        player.SetActive(GameState.gs.skipCutscene);
        if (testOutDeath) defaultSpeed = 2; slowThresholdSpeed = 2;
        //defaultSpeed = ceilingSpeedScale(defaultSpeed, 0f, 10f, 0f, 0.5f);
        ceilingSpeed = defaultSpeed;
        currRoom = FindObjectOfType<Room_Main>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
        ceilingSourceChild = player.transform.GetChild(3).gameObject;
    }

    private void FixedUpdate()
    {
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
        }

        if (GameState.gs.introFinished == true) //|| AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce == true)
        {
            if (!doOnce)
            {
                doOnce = true;
                dustStorm.SetActive(false);
                l.gameObject.SetActive(false);
                Timer.timer.countTime = true;
            }
            Debug.Log("ceiling is moving");

            ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
            //Fmod stuff
            ceilingSourceChild.transform.position = new Vector3(player.transform.position.x, ceiling.transform.position.y - 0.5f, player.transform.position.z);
            AudioManager.am.ceilingLoopInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
            AudioManager.am.ceilingFBDebrisInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(ceilingSourceChild));
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Height_Y", ceilingSourceChild.transform.position.y);
            //Debug.Log("Height: " + ceilingSourceChild.transform.position.y);
        }
    }

    /*public static float ceilingSpeedScale(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }*/
    private void Update()
    {
        //Debug control variables
        //Debug.Log("intro finished " + GameState.gs.introFinished + "|| skip cutscene " + GameState.gs.skipCutscene + "|| fmod restart" + AudioManager.am.FMODRestarted);
        
        if (Input.GetKeyDown(KeyCode.Tab)) Pause();
    }

    private void OnDeath()
    {
        isDead = true;
        Timer.timer.countTime = false;
        if (roomsCleared > PlayerPrefs.GetInt("roomsCleared")) PlayerPrefs.SetInt("roomsCleared", roomsCleared);
        scoreText.text = "you cleared " + roomsCleared + " rooms!\n your highscore is " + PlayerPrefs.GetInt("roomsCleared") + "\nyou survived for " + Timer.timer.time +  "\npress r to restart\npress m to go to the main menu\npress esc to quit";
        deathScreen.SetActive(true);
        player.SetActive(false);
        AudioManager.am.FMOD_DeadState();
    }

    public void Pause()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        pauseScreen.SetActive(!pauseScreen.activeSelf);
        player.SetActive(!player.activeSelf);
        if(!player.activeInHierarchy) AudioManager.am.pauseSSInstance.start();
        else AudioManager.am.pauseSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}