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
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameState.gs.skipCutscene = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
