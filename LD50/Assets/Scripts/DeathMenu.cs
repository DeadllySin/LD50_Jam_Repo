using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameState.gs.skipCutscene = true;
            AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = false;
            AudioManager.am.FMODRestarted = true;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameState.gs.skipCutscene = false;
            AudioManager.am.FMOD_MainMenuState();
            GameState.gs.introFinished = false;
            AudioManager.am.FMODRestarted = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
