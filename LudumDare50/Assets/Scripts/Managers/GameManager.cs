using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Main")]
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
    public float speedBoost, ceilingSpeed;
    [SerializeField] private float defaultSpeed, deathHeight, thresholdToSlower, fastSpped, slowThresholdSpeed;
    [HideInInspector] public GameObject ceilingSourceChild;
    private bool doOnce;
    private float startTime;
    private string minutes, seconds;


    private void Awake()
    {
        dustStorm.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        playerCine.SetActive(GameState.gs.skipCutscene);
        cutsceneCam.SetActive(!GameState.gs.skipCutscene);
        cutscene.SetActive(!GameState.gs.skipCutscene);
        door.SetActive(!GameState.gs.skipCutscene);
        player.SetActive(GameState.gs.skipCutscene);
        mainMenu.SetActive(!GameState.gs.skipCutscene);
        startTime = Time.time;
        ceilingSpeed = defaultSpeed;
        currRoom = FindObjectOfType<Room_Main>().gameObject;
        currTunnel = FindObjectOfType<Tunnel>();
        ceilingSourceChild = player.transform.GetChild(3).gameObject;
        FindObjectOfType<EventSystem>().enabled = true;
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
                    //ceilingSpeed = fastSpped;
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
                    //l.gameObject.SetActive(false);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameState.gs.introFinished && !mainMenu.activeInHierarchy) Pause();
    }

    private void OnDeath()
    {
        FindObjectOfType<EventSystem>().enabled = true;
        //Debug.Log("OnDeath Game Manager");
        Destroy(FindObjectOfType<Room_Main>().gameObject);
        isDead = true;
        Timer.timer.countTime = false;
        if (roomsCleared > PlayerPrefs.GetInt("roomsCleared")) PlayerPrefs.SetInt("roomsCleared", roomsCleared);
        scoreText.text = "score " + roomsCleared + "\nhighscore " + PlayerPrefs.GetInt("roomsCleared");
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
        if (!canPause) return;
        canPause = false;
        StartCoroutine(pauseCool());
        if (pauseScreen.activeInHierarchy)
        {
            FindObjectOfType<EventSystem>().enabled = (false);
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
            pauseScreen.SetActive(false);
            player.SetActive(true);
        }
        else
        {
            FindObjectOfType<EventSystem>().enabled = (true);
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
            pauseScreen.SetActive(true);
            player.SetActive(false);
        }

        //if(!player.activeInHierarchy) AudioManager.am.pauseSSInstance.start();
        //else AudioManager.am.pauseSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    bool canPause = true;

    IEnumerator pauseCool()
    {
        canPause = false;
        yield return new WaitForSeconds(.6f);
        canPause = true;
    }
}