using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //resume
        {
            M_Return();
        }
    }

    public void M_Return()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
    }

    public void M_MainMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        AudioManager.am.FMOD_MainMenuState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameState.gs.skipCutscene = false;
        GameState.gs.introFinished = false;
        this.enabled = false;
    }

    public void M_Quit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        Application.Quit();
    }

    }
