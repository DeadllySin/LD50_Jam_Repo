using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAndURLLoader : MonoBehaviour
{
    private Menu_Events m_PauseMenu;


    private void Awake()
    {
        m_PauseMenu = GetComponentInChildren<Menu_Events>();
    }


    public void SceneLoad(string sceneName)
    {
        //PauseMenu pauseMenu = (PauseMenu)FindObjectOfType(typeof(PauseMenu));
        //m_PauseMenu.MenuOff ();
        SceneManager.LoadScene(sceneName);
    }


    public void LoadURL(string url)
    {
        Application.OpenURL(url);
    }
}

