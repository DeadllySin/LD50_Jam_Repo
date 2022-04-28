using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void M_PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameState.gs.menuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameState.gs.menuMusicInstance.release();
    }

    public void M_QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
