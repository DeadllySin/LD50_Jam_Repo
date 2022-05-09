using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //resume
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameState.gs.skipCutscene = true;
            //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
            //AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger = false;            
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameState.gs.skipCutscene = false;
            GameState.gs.introFinished = false;
            this.enabled = false;
            AudioManager.am.FMOD_MainMenuState();
        }
    }
}