using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainMenu_Main : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    public CanvasGroup mainMenuUIGroup;
    [SerializeField] private Text versionText;
    [HideInInspector] public bool fadeIn = false;
    [HideInInspector] public bool fadeOut = false;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        versionText.text = "Version " + Application.version;
        slider.value = PlayerPrefs.GetFloat("vol");
    }
    private void Update()
    {
        if (!this.gameObject.activeInHierarchy || GameState.gs.introFinished || GameState.gs.skipCutscene) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        }

    }

    private void FixedUpdate()
    {
        if (fadeIn == true)
        {
            if (mainMenuUIGroup.alpha < 1)
            {
                mainMenuUIGroup.alpha += Time.deltaTime * 0.5f;
                if (mainMenuUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut == true)
        {
            if (mainMenuUIGroup.alpha >= 0)
            {
                var seconds = A_Timer.a_timer.a_s * 0.1f;
                mainMenuUIGroup.alpha -= Time.deltaTime * seconds;
                //Debug.Log(seconds);
                if (AudioManager.am.startTimerCB == false)
                {
                    fadeOut = false;
                    this.gameObject.SetActive(false);
                    A_Timer.a_timer.a_s = 0;
                }
            }
        }
    }

    private void Start()
    {
        mainMenuUIGroup.alpha = 0;
        fadeIn = true;
    }
    private void OnEnable()
    {
        mainMenuUIGroup.alpha = 1f;
    }

    public void StartGame()
    {
        FindObjectOfType<EventSystem>().enabled = false;
        GameState.gs.introFinished = false;
        AudioManager.am.startTimerCB = true;
        fadeOut = true;
        AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = true;
        AudioManager.am.FMOD_InGameState(); //n�o mexer que est� ligado �s vari�veis call back
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        this.enabled = false;
    }

    public void OnValueChanged(Slider sl)
    {
        AudioManager.am.masterBus.setVolume(sl.value);
        PlayerPrefs.SetFloat("vol", sl.value);
    }

    public void FMOD_Click()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
