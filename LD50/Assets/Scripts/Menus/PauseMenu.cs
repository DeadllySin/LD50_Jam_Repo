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
        if (this.enabled)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
            }
        }
    }

    public void M_Return()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        GameManager.gm.Pause();
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