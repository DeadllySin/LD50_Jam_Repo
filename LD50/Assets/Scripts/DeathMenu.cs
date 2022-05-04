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
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameState.gs.skipCutscene = false;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
