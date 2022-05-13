using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Main : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject howToPlay;

    public CanvasGroup mainMenuUIGroup;
    [HideInInspector] public bool fadeIn = false;
    [HideInInspector] public bool fadeOut = false;

    private void Update()
    {
        if (this.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
            }
        }
    }

    private void FixedUpdate()
    {
        if (fadeIn == true)
        {
            Debug.Log("fade out false ");
            if (mainMenuUIGroup.alpha < 1)
            {
                mainMenuUIGroup.alpha += Time.deltaTime;
                if (mainMenuUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut == true)
        {
            Debug.Log("fade out tru ");
            if (mainMenuUIGroup.alpha >= 0)
            {
                //delta time is 0.02
                var seconds = A_Timer.a_timer.a_s * 0.1f;
                mainMenuUIGroup.alpha -= Time.deltaTime * seconds;
                //mainMenuUIGroup.alpha -= (Mathf.Sin(Time.time * A_Timer.a_timer.a_ms) + 1.0f) / 2.0f; 
                //Debug.Log("ALPHA IS " + mainMenuUIGroup.alpha);
                //Debug.Log("TIME IS " + A_Timer.timer.s);
                Debug.Log(seconds);
                if (AudioManager.am.startTimerCB == false)
                {
                    Debug.Log("stop counter");
                    fadeOut = false;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnEnable()
    {
        mainMenuUIGroup.alpha = 1f;
    }
    
    public void StartGame()
    {
        GameState.gs.introFinished = false;
        //mainMenuUIGroup.alpha = 1f;
        AudioManager.am.startTimerCB = true;
        fadeOut = true;
        AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = true;
        AudioManager.am.FMOD_InGameState(); //não mexer que está ligado ás variáveis call back
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        //AudioManager.am.FMOD_LoadInGameInstances();
    }

    public void Credits()
    {
        credits.SetActive(true);
        this.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
    }

    public void OnValueChanged(Slider sl)
    {
        AudioManager.am.masterBus.setVolume(sl.value);
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        this.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
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
