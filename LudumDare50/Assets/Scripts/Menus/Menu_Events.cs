using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Events : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameObject[] menus;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //resume
        {
            M_Return();
        }
        if (GameState.gs.introFinished || GameState.gs.skipCutscene || !gm.isDead) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
        }
    }

    public void OnValueChanged(Slider sl)
    {
        AudioManager.am.masterBus.setVolume(sl.value);
        PlayerPrefs.SetFloat("vol", sl.value);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameState.gs.skipCutscene = true;
        AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = false;
        AudioManager.am.FMODRestarted = true;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
    }

    public void M_Return()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        gm.Pause();
    }

    public void MainMenu()
    {
        GameState.gs.introFinished = false;
        //AudioManager.am.FMODRestarted = false;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        AudioManager.am.FMOD_MainMenuState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameState.gs.skipCutscene = false;
        GameState.gs.introFinished = false;
    }

    public void Quit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        Application.Quit();
    }

    public void ChangeMenu(string menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].name != menu) menus[i].SetActive(false);
            else menus[i].SetActive(true);
        }
    }
}