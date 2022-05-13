using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Main : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject howToPlay;

    [SerializeField] private CanvasGroup mainMenuUIGroup;
    [SerializeField] private bool fadeIn;
    [SerializeField] private bool fadeOut;

    private void Update()
    {
        if (this.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
            }
        }
        
        if (fadeIn == true)
        {
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
            if (mainMenuUIGroup.alpha >= 0)
            {
                Debug.Log(mainMenuUIGroup.alpha -= Timer.timer.ms * 0.001f);
                //mainMenuUIGroup.alpha -= Time.deltaTime;
                mainMenuUIGroup.alpha -= Timer.timer.ms * 0.001f;
                if (mainMenuUIGroup.alpha == 1)
                {
                    fadeOut = false;
                    AudioManager.am.startTimerCB = false;
                }
            }
        }
    }

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }
    public void StartGame()
    {
        //cutscene.GetComponent<Animator>().SetTrigger("play");
        this.gameObject.SetActive(false);
        GameState.gs.introFinished = false;
        AudioManager.am.startTimerCB = true;
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
