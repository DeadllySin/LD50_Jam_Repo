using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //resume
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameState.gs.skipCutscene = true;
            //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
            //AudioManager.am.GetComponent<A_MusicCallBack>().musicIntroTrigger = false;            
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameState.gs.skipCutscene = false;
            GameState.gs.introFinished = false;
            this.enabled = false;
            AudioManager.am.FMOD_MainMenuState();
        }
    }
}
